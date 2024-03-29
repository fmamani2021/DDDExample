﻿using VeterinaryClinic.ManagementModule.Shared.DTOs.Room;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class RoomService
    {
        private readonly IHttpServiceManagement _httpService;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IHttpServiceManagement httpService, ILogger<RoomService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<RoomDto> CreateAsync(CreateRoomRequest room)
        {
            _logger.LogInformation($"Creating room: {room}");
            return (await _httpService.HttpPostAsync<CreateRoomResponse>("rooms", room)).Room;
        }

        public async Task<RoomDto> EditAsync(UpdateRoomRequest room)
        {
            return (await _httpService.HttpPutAsync<UpdateRoomResponse>("rooms", room)).Room;
        }

        public async Task DeleteAsync(int roomId)
        {
            await _httpService.HttpDeleteAsync<DeleteRoomResponse>("rooms", roomId);
        }

        public async Task<RoomDto> GetByIdAsync(int roomId)
        {
            return (await _httpService.HttpGetAsync<GetByIdRoomResponse>($"rooms/{roomId}")).Room;
        }

        public async Task<List<RoomDto>> ListPagedAsync(int pageSize)
        {
            _logger.LogInformation("Fetching rooms from API.");

            return (await _httpService.HttpGetAsync<ListRoomResponse>($"rooms")).Rooms;
        }

        public async Task<List<RoomDto>> ListAsync()
        {
            _logger.LogInformation("Fetching rooms from API.");

            return (await _httpService.HttpGetAsync<ListRoomResponse>($"rooms")).Rooms;
        }
    }
}
