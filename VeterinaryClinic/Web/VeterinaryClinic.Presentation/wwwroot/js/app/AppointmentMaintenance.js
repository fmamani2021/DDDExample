function AppointmentMaintenance(config) {
    this.viewConfigs = config;    
}

AppointmentMaintenance.prototype = {
    onReady: function () {
        let self = this;
        var dataSource = new kendo.data.SchedulerDataSource({
            transport: {
                read: {
                    url: `${viewConfigs.urls.read}?scheduleId=${viewConfigs.scheduleId}`,
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
                    if (operation == "create") {
                        var modelEmpty = {};
                        modelEmpty.appointmentId = kendo.guid();
                        modelEmpty.ScheduleId = viewConfigs.scheduleId
                        modelEmpty.RoomId = options.roomId;
                        modelEmpty.DoctorId = options.doctorId;
                        modelEmpty.ClientId = options.clientId;
                        modelEmpty.PatientId = options.patientId;
                        modelEmpty.Start = options.start;
                        modelEmpty.End = options.end;
                        modelEmpty.Title = options.title;                        
                        modelEmpty.Description = options.description ? options.description : "" ;                        
                        modelEmpty.IsAllDay = false;
                        modelEmpty.IsPotentiallyConflicting = false;
                        modelEmpty.IsConfirmed = false;
                        modelEmpty.AppointmentTypeId = options.appointmentTypeId;   
                        modelEmpty.PatientName = "";
                        modelEmpty.ClientName = "";
                        modelEmpty.AppointmentType = null;
                        return JSON.stringify(modelEmpty);
                    }
                    if (operation == "update" || operation == "destroy") {
                        return JSON.stringify(options);
                    }
                }
            },
            schema: {
                model: {
                    id: "appointmentId",
                    fields: {
                        appointmentId: { from: "appointmentId", type: "string" },
                        title: { from: "title", validation: { required: true } },
                        start: { type: "date", from: "start" },
                        end: { type: "date", from: "end" },
                        description: { from: "description" },
                        roomId: { from: "roomId", nullable: true, type: "number" },
                        isAllDay: { type: "boolean", from: "isAllDay" },
                        ScheduleId: { type: "string", from: "ScheduleId" },
                        isConflict: { type: "boolean", from: "isPotentiallyConflicting" },
                        isConfirmed: { from: "isConfirmed" }
                    }
                }
            },
                requestStart: function (e) {
                    kendo.ui.progress($('#view_schedule').find('.k-scheduler-agenda'), true);
                },
                requestEnd: function (e) {
                    kendo.ui.progress($('#view_schedule').find('.k-scheduler-agenda'), false);
                },
            autoBind: false,
            batch: false
        });

        var viewModel = kendo.observable({            
            scheduler: {   
                selectedDate: new Date("2030/9/23 8:00"),
                doctors: {
                    dataSource: viewConfigs.dataSources.doctors
                },
                clients: {
                    dataSource: viewConfigs.dataSources.clients,
                    change: function (event, dataItem) {
                        if (event && event.preventDefault)
                            event.preventDefault();

                        if (dataItem.preferredDoctorId) {
                            var schedule = $("#custom_scheduler").data("kendoScheduler");
                            schedule.editable.options.model.set("doctorId", dataItem.preferredDoctorId);
                        }
                        
                        if (dataItem.clientId) {
                            var url = `${viewConfigs.urls.listByClientId}?clientId=${dataItem.clientId}`;
                            var patientsService = AjaxGetPromiseAsync(url);
                            patientsService.done(function (patients) {
                                if (patients) {
                                    viewModel.scheduler.patients.set("dataSource", patients);
                                } else {
                                    viewModel.scheduler.patients.set("dataSource", []);                        
                                }   
                                viewModel.scheduler.patients.set("isVisible", true);
                            }).fail(function (response) {
                                viewModel.scheduler.patients.set("isVisible", false);
                                viewModel.scheduler.patients.set("dataSource", []);                        
                            });                            
                            return;
                        }
                        viewModel.scheduler.patients.set("isVisible", false);                        
                        viewModel.scheduler.patients.set("dataSource", []);                         
                    }
                },
                patients: {
                    dataSource: [],
                    isVisible: false
                },
                rooms: {
                    dataSource: viewConfigs.dataSources.rooms
                },
                appointmentTypes: {
                    dataSource: viewConfigs.dataSources.appointmentTypes,
                    change: function (event, dataItem) {
                        if (event && event.preventDefault)
                            event.preventDefault();

                        if (dataItem.duration) {
                            var schedule = $("#custom_scheduler").data("kendoScheduler");
                            var newDuration = viewModel.addMinutes(schedule.editable.options.model.start, dataItem.duration);
                            schedule.editable.options.model.set("end", newDuration);
                        }
                    }
                },                
                dataSource: dataSource,
                events: {
                    dataBinding: function (e) {
                        //console.log("dataBinding");
                    },
                    dataBound: function (e) {
                        //console.log("dataBound");
                    },                    
                    save: function (e) {
                        var windowsModel = $(e.container)
                        //console.log("scheduler_save ------> ");
                    },
                    edit: function (e) {
                        let room = viewModel.scheduler.rooms.dataSource.filter(p => { return p.roomId == e.event.roomId })[0];

                        let title = `Create Appointment - Room: ${room.name}`;
                        var appointmentId = $(e.container).data().kendoEditable.options.model.appointmentId;
                        if (appointmentId) {
                            title = `Edit Appointment - Room: ${room.name}`;
                        }
                        $(e.container).data().kendoWindow.setOptions({
                            title: title
                        });
                        
                        var dropDownClient = $("#ddlClient").data("kendoDropDownList")
                        dropDownClient.bind("change", function (event) {
                            viewModel.scheduler.clients.change(event, this.dataItem());
                        });

                        var dropDownAppointmentType = $("#ddlAppointmentType").data("kendoDropDownList")
                        dropDownAppointmentType.bind("change", function (event) {
                            viewModel.scheduler.appointmentTypes.change(event, this.dataItem());
                        });

                        if (e.event.clientId) {
                            var url = `${viewConfigs.urls.listByClientId}?clientId=${e.event.clientId}`;
                            var patientsService = AjaxGetPromiseAsync(url);
                            patientsService.done(function (patients) {
                                if (patients) {
                                    viewModel.scheduler.patients.set("dataSource", patients);
                                } else {
                                    viewModel.scheduler.patients.set("dataSource", []);
                                }
                                viewModel.scheduler.patients.set("isVisible", true);
                            }).fail(function (response) {
                                viewModel.scheduler.patients.set("isVisible", false);
                                viewModel.scheduler.patients.set("dataSource", []);
                            });                          
                        }                        
                    },
                    remove: function (e) {
                        //console.log("scheduler_remove ------> ");
                    },
                    add: function (e) {
                        //console.log("scheduler_add ------> ");
                    }                    
                },
                startTime: new Date("2023/3/5 7:00"),
                endTime: new Date("2023/3/5 20:00"),
            },
            addMinutes: function(date, minutes) {
                return new Date(date.getTime() + minutes * 60000);
            }
        });
        self.viewModel = viewModel;
        kendo.bind($("#view_scheduler"), self.viewModel)

        viewConfigs.signalrConnection.on("ReceiveMessage", function (notification) {
            var messageResponse = JSON.parse(notification);
            if (messageResponse.code == 'client-updated') {
                alertify.success(messageResponse.message);
            }            
        });

        var scheduler = $("#custom_scheduler").data("kendoScheduler");
        if (scheduler != null) {
            scheduler.options.startTime = new Date("2030/9/23 8:00");
            scheduler.options.endTime = new Date("2030/9/23 20:00");
            scheduler.options.footer = false;
            scheduler.date(new Date("2030/9/23"));            
        }

        
    }
}