using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Utilities.Enums
{
    
    public enum StatusLevel
    {

        Requested,
        Approved,
        Rejected,
        Canceled,
        Completed,
        [Display(Name = "On Going")] OnGoing //Enum tidak bisa spasi, Bisa Pakai anotasi (Display Name)
    }
}