namespace UKParliament.CodeTest.Services
{
    public class Constants
    {
        public class ErrorMessages
        {
            public const string NotAdded = "Not added";
            public const string NotUpdated = "Not updated";
            public const string InformationError = "Information error";
            public const string NotFound = "Not found";
            public const string IdIncorrect = "Id incorrect";
            public const string SearchParametersIncorrect = "Search parameter(s) incorrect";
            public const string InsufficientInformation = "insufficient information";

            public class DataRepo
            {
                public const string ErrorAddingEntity = "Error adding entity";
                public const string EntityExist = "Entity exist";
                public const string EntityDoesNotExist = "Entity does not exist";
                public const string EntityUpdateFailed = "Entity update failed";
                public const string EntityDeleteFailed = "Entity delete failed";
                public const string EntityDeletionNotPermitted = "Deletion not permitted";
                public const string NoRoomAvailable = "No room available";
                public const string BookingRemovalFailed = "Booking removal failed";
            }
        }
    }
}
