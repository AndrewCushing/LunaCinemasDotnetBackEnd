namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class ResponseText
    {
        public static readonly string SuccessfullyRetrievedNewFilms = "Retrieved all newly released films";

        public static readonly string UnableToRetrieveNewFilms =
            "Unable to retrieve new films. Please check your connection and try again.";

        public static readonly string SuccessfullyRetrievedUpcomingFilms = "Retrieved all soon to be released films";

        public static readonly string UnableToRetrieveUpcomingFilms =
            "Unable to retrieve upcoming films. Please check your connection and try again.";

        public static readonly string ReviewSubmittedWordsWereCensored = "Review submitted successfully. Words were censored.";
    }
}