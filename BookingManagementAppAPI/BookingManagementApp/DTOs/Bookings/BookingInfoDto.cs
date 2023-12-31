﻿using BookingManagementApp.Utilities.Enums;

namespace BookingManagementApp.DTOs
{
    //DTO untuk mapping data Info Booking
    public class BookingInfoDto
    {
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public StatusLevel Status { get; set; }
        public int Floor { get; set; }
        public string BookedBy { get; set; }
    }

}
