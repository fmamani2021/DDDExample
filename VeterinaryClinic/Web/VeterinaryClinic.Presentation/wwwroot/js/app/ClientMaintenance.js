function ClientMaintenance(config) {
    this.viewConfigs = config;
}

ClientMaintenance.prototype = {
    onReady: function () {
        let self = this;

        var clientDatasource = new kendo.data.DataSource({
            schema: {
                model: {
                    id: "clientId",
                    fields: {
                        clientId: { type: "number" },
                        fullName: { type: "string" },
                        preferredName: { type: "string" },
                        salutation: { type: "string" },
                        emailAddress: { type: "string" },
                        preferredDoctorId: { type: "number" },
                        status: { type: "string" },
                    }
                }
            },
            batch: true,
            pageSize: 10,
            /*autoBind: false,*/
            transport: {
                read: {
                    url: `${viewConfigs.urls.read}`,
                    type: "GET",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                },
                create: {
                    url: viewConfigs.urls.create,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                update: {
                    url: viewConfigs.urls.update,
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                destroy: {
                    url: viewConfigs.urls.delete,
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models && options.models.length) {
                        return JSON.stringify(options.models[0]);                        
                    }
                }
            },
            requestStart: function (e) {
                kendo.ui.progress($('#view_client'), true);
            },
            requestEnd: function (e) {
                kendo.ui.progress($('#view_client'), false);
            },
        })

        var viewModel = kendo.observable({
            client: {
                dataSource: clientDatasource,
                doctors: {
                    dataSource: viewConfigs.dataSources.doctors
                },
                actions: {
                    save: function (e) {
                        viewModel.patient.removed.forEach(function (item, index) {
                            e.model.patients.push(item);
                        });

                        //console.log("grid_save ------> ");
                    },
                    add: function (e) {
                        //console.log("grid_add ------> ");
                    },
                    edit: function (e) {
                        //console.log("grid_edit ------> ");
                        
                        let title = 'Create Client';
                        var clientId = $(e.container).data().kendoEditable.options.model.clientId;
                        if (clientId) {
                            title = 'Edit Client';
                        }
                        $(e.container).data().kendoWindow.setOptions({
                            title: title
                        });

                        $("#grid_patients").kendoGrid({
                            dataSource: viewModel.patient.dataSource(e.model.patients, e.model.clientId),
                            toolbar: ["create"],
                            columns: [
                                { field: 'patientId', hidden: 'true' },
                                { field: 'clientId', hidden: 'true' },
                                { field: 'name', title: 'Name', width: '20%' },
                                { field: 'sex', title: 'Sex', width: '10%' },
                                { field: 'species', title: 'Specie', width: '20%' },
                                { field: 'breed', title: 'Breed', width: '20%' },
                                { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
                            editable: "inline"
                        });

                        viewModel.patient.removed = [];
                    },
                    remove: function (e) {
                        //console.log("grid_remove ------> ");
                    }
                }                
            },
            patient: {
                removed : [],
                newPatientId: -1,
                dataSource: function (patients, clientId) {
                    return new kendo.data.DataSource({
                        data: patients,
                        transport: {
                            read: function (options) {
                                options.success(patients);
                            },
                            update: function (options) {
                                //console.log("update", options);
                                debugger;
                                options.data.status = "Update";

                                let grid = $("#grid_clients").data("kendoGrid");
                                let model = grid.dataSource.data().filter(p => p.clientId == clientId)[0];
                                model.dirty = true;
                                
                                options.success(options.data);
                            },
                            destroy: function (options) {    
                                debugger;
                                let itemToRemove = viewModel.patient.removed.filter(p => p.patientId == options.patientId)[0]
                                if (!itemToRemove) {
                                    options.data.status = "Delete";
                                    viewModel.patient.removed.push(options.data);
                                }
                                
                                //console.log("destroy", options);
                                let grid = $("#grid_clients").data("kendoGrid");
                                let model = grid.dataSource.data().filter(p => p.clientId == clientId)[0];
                                model.dirty = true;

                                options.success();
                            },
                            create: function (options) {
                                debugger;
                                //console.log("create", options);
                                options.data.clientId = clientId;

                                options.data.status = "Create";

                                viewModel.patient.newPatientId = viewModel.patient.newPatientId - 1;
                                options.data.patientId = viewModel.patient.newPatientId
                                options.success(options.data);
                            }
                        },
                        schema: {
                            model: {
                                id: "patientId",
                                fields: {
                                    patientId: { type: "number", editable: false },
                                    clientId: { type: "number", editable: false },
                                    name: { type: "string" },
                                    sex: { type: "string" },
                                    species: { type: "string" },
                                    breed: { type: "string" }
                                }
                            }
                        }
                    });
                }
            } 
            
        });

        self.viewModel = viewModel;
        kendo.bind($("#view_client"), self.viewModel)
    }
}