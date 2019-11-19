using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.BusinessLogic

{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class InitialisationHandler
    {
        private readonly IFilmContext _filmRepo;
        private readonly ReviewFilter _reviewHandler;
        private readonly CommentFilter _commentRepo;
        private readonly ShowingService _showRepo;
        private readonly ICustomerContext _userRepo;
        private readonly IAdminContext _adminRepo;
        private readonly IAccessTokenContext _accessTokenContext;
        private readonly AccountCreationService _accountCreationService;
        private readonly ExistingUserService _existingUserService;

        public InitialisationHandler(IFilmContext filmRepo, ReviewFilter reviewRepo, CommentFilter commentRepo, ShowingService showRepo,
        ICustomerContext userRepo, IAdminContext adminRepo, IAccessTokenContext accessTokenContext, 
        AccountCreationService accountCreationService, ExistingUserService existingUserService)
        {
            _filmRepo = filmRepo;
            _reviewHandler = reviewRepo;
            _commentRepo = commentRepo;
            _showRepo = showRepo;
            _userRepo = userRepo;
            _adminRepo = adminRepo;
            _accessTokenContext = accessTokenContext;
            _existingUserService = existingUserService;
            _accountCreationService = accountCreationService;
        }

        public ActionResult<ResponseObject<object>> InitialiseData()
        {
            InitialiseFilms();
            InitialiseReviews();
            InitialiseComments();
            InitialiseShowings();
            InitialiseUsers();
            return new ResponseObject<object>(true, "Initialised data", null);
        }

        private void InitialiseUsers()
        {
            _accessTokenContext.DeleteAll();
            _userRepo.DeleteAll();
            _adminRepo.DeleteAll();
            _accountCreationService.AddAdmin(new List<string> { "Bill", "Gates", "thebawss@microsoft.com", "0123456789012345678901234567890123456789012345678901234567890123" });
        }

        private void InitialiseFilms()
        {
            _filmRepo.DeleteAll();
            InitialiseUpcomingFilms1();
            InitialiseCurrentFilms();
        }

        private void InitialiseCurrentFilms()
        {
            _filmRepo.AddFilm(new Film
            {
                Title = ("The Current War"),
                BriefDescription = ("The dramatic story of the cutthroat race between electricity titans Thomas Edison and George Westinghouse to determine whose electrical system would power the modern world."),
                DetailedDescription = ("Thomas Edison and George Westinghouse – one a brilliant inventor, the other a hugely successful businessman – fought each other in ‘The Current War’, over whose electricity system would power the world.  Whoever won this battle had the potential to control everything: lighting, communication and transportation could all be powered by this revolutionary new technology. And in the bitter, cutthroat competition that ensued, only one could ever come out on top.  A riveting drama about a defining moment in the history of technology,"),
                ImagePath = ("/CurrentFilmImages/CurrentWar.jpg"),
                Year = ("2019"),
                Length = ("1h 48m"),
                Classification = ("Class15"),
                Directors = (new [] { "Alfonso Gomez-Rejon" }),
                Genres = (new [] { "Drama" }),
                Actors = (new [] { "Tom Holland", "Benedict Cumberbatch", "Katherine Waterston", "Tuppence Middleton" }),
                IsReleased = (true)
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "The Lion King",
                BriefDescription = "This Disney animated feature follows the adventures of the young lion Simba, the heir of his father, Mufasa. Simba's wicked uncle, " +
                                   "Scar, plots to usurp Mufasa's throne by luring father and son into a stampede of wildebeests.",
                DetailedDescription = "Simba idolizes his father, King Mufasa, and takes to heart his own royal destiny on the plains of Africa. But not everyone in the " +
                                      "kingdom celebrates the new cub's arrival. Scar, Mufasa's brother -- and former heir to the throne -- has plans of his own. The " +
                                      "battle for Pride Rock is soon ravaged with betrayal, tragedy and drama, ultimately resulting in Simba's exile. Now, with help from a " +
                                      "curious pair of newfound friends, Simba must figure out how to grow up and take back what is rightfully his.",
                ImagePath = "/CurrentFilmImages/Lionking.jpg",
                Year = "2019",
                Length = "2h 0m",
                Classification = "ClassPG",
                Directors = new[] {"Jon Favreau"},
                Genres = new[] {"Family", "Musical"},
                Actors = new[]
                {
                    "Donald Glover, Seth Rogen, Chiwetel Ejiofor, Alfre Woodard, Billy Eichner, John Kani, John Oliver, Florence Kasumba, Eric Andre, Keegan-Michael Key, JD McCrary, Shahadi Wright Joseph, Beyonce Knowles-Carter, James Earl Jones"
                },
                IsReleased = true
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Spider-Man: Far From Home",
                BriefDescription =
                    "Following the events of Avengers: Endgame, Spider-Man must step up to take on new threats in a world that has changed forever.",
                DetailedDescription =
                    "Peter Parker returns in Spider-Man: Far From Home, the next chapter of the Spider-Man: Homecoming series! Our friendly neighbourhood Super Hero " +
                    "decides to join his best friends Ned, MJ, and the rest of the gang on a European vacation. However, Peter's plan to leave super heroics behind for a " +
                    "few weeks are quickly scrapped when he begrudgingly agrees to help Nick Fury uncover the mystery of several elemental creature attacks, creating havoc " +
                    "across the continent! Following the events of Avengers: Endgame, Spider-Man must step up to take on new threats in a world that has changed forever.",
                ImagePath = "/CurrentFilmImages/Spider.jpg",
                Year = "2019",
                Length = "2h 09m",
                Classification = "Class12A",
                Directors = new [] {"Jon Watts"},
                Genres = new [] {"Action", "Superhero"},
                Actors = new []
                {
                    "Tom Holland, Samuel L. Jackson, Zendaya, Cobie Smulders, Jon Favreau, JB Smoove, Jacob Batalon, Martin Starr, Marisa Tomei, Jake Gyllenhaal"
                },
                IsReleased = true
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Toy Story 4",
                BriefDescription =
                    "Woody, Buzz Lightyear and the rest of the gang embark on a road trip with Bonnie and a new toy named Forky.",
                DetailedDescription =
                    "Woody (voice of Tom Hanks) has always been confident about his place in the world, and that his priority is " +
                    "taking care of his kid, whether that's Andy or Bonnie. So when Bonnie's beloved new craft-project-turned-toy, " +
                    "Forky (voice of Tony Hale), declares himself as 'trash' and not a toy, Woody takes it upon himself to show Forky " +
                    "why he should embrace being a toy. But when Bonnie takes the whole gang on her family's road trip excursion, Woody " +
                    "ends up on an unexpected detour that includes a reunion with his long-lost friend Bo Peep (voice of Annie Potts). " +
                    "After years of being on her own, Bo's adventurous spirit and life on the road belie her delicate porcelain exterior. " +
                    "As Woody and Bo realize they're worlds apart when it comes to life as a toy, they soon come to find that's the least " +
                    "of their worries.",
                ImagePath = "/CurrentFilmImages/Toystory.jpg",
                Year = "2019",
                Length = "1h 40m",
                Classification = "ClassU",
                Directors = new[] {"Josh Cooley"},
                Genres = new[] {"Family", "Animated Cartoon"},
                Actors = new[]
                {
                    "Tom Hanks, Tim Allen, Annie Potts, Tony Hale, Keegan-Michael Key, Maddie McGraw, Christina Hendricks, Jordan Peele, Keanu Reeves, " +
                    "Ally Maki, Jay Hernandez, Lori Alan, Joan Cusack, Bonnie Hunt, Kristen Schaal, Emily Davis, Wallace Shawn, John Ratzenberger, " +
                    "Blake Clark, June Squibb, Carl Weathers, Lila Sage, Don Rickles, Jeff Garlin, Maliah Bargas-Good, Jack McGraw, Juliana Hansen, " +
                    "Estelle Harris"
                },
                IsReleased = true
            });
            InitialiseCurrentFilms2();
        }

        private void InitialiseCurrentFilms2()
        {
            _filmRepo.AddFilm(new Film
            {
                Title = "Fast & Furious: Hobbs & Shaw",
                BriefDescription =
                    "Lawman Luke Hobbs and outcast Deckard Shaw form an unlikely alliance when a cyber-genetically enhanced villain threatens the future of humanity.",
                DetailedDescription =
                    "After eight films that have amassed almost $5 billion worldwide, the Fast & Furious franchise now features its first stand-alone vehicle as Dwayne Johnson and Jason Statham reprise their roles as Luke Hobbs and Deckard Shaw in Fast & Furious: Hobbs & Shaw. Ever since hulking lawman Hobbs (Johnson), a loyal agent of America's Diplomatic Security Service, and lawless outcast Shaw (Statham), a former British military elite operative, first faced off in 2015's Fast & Furious 7, the duo have swapped smack talk and body blows as they've tried to take each other down. But when cyber-genetically enhanced anarchist Brixton (Idris Elba) gains control of an insidious bio-threat that could alter humanity forever — and bests a brilliant and fearless rogue MI6 agent (The Crown's Vanessa Kirby), who just happens to be Shaw's sister - these two sworn enemies will have to partner up to bring down the only guy who might be badder than themselves. Hobbs & Shaw blasts open a new door in the Fast universe as it hurtles action across the globe, from Los Angeles to London and from the toxic wasteland of Chernobyl to the lush beauty of Samoa.",
                ImagePath = "/CurrentFilmImages/FastAndFurious.jpg",
                Year = "2019",
                Length = "2h 14m",
                Classification = "Class12",
                Directors = new[] {"David Leitch"},
                Genres = new[] {"Action", "Adventure"},
                Actors = new[] {"Dwayne Johnson, Jason Statham, Vanessa Kirby, Idris Elba"},
                IsReleased = true
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "The Angry Birds Movie 2",
                BriefDescription =
                    "Flightless birds lead a mostly happy existence, except for Red (Jason Sudeikis), who just can't get past the daily annoyances of life. His temperament leads him to anger management class, where he meets fellow misfits Chuck (Josh Gad) and Bomb.",
                DetailedDescription =
                    "The flightless angry birds and the scheming green piggies take their beef to the next level in The Angry Birds Movie 2! When a new threat emerges that puts both Bird and Pig Island in danger, Red (Jason Sudeikis), Chuck (Josh Gad), Bomb (Danny McBride), and Mighty Eagle (Peter Dinklage) recruit Chuck's sister Silver (Rachel Bloom) and team up with pigs Leonard (Bill Hader), his assistant Courtney (Awkwafina), and techpig Garry (Sterling K. Brown) to forge an unsteady truce and form an unlikely superteam to AddFilm their homes.",
                ImagePath = "/CurrentFilmImages/Angrybirds.jpg",
                Year = "2019",
                Length = "1h 35m",
                Classification = "ClassU",
                Directors = new [] {"Tony Leondis, John Rice"},
                Genres = new [] {"Action", "Adventure", "Animation"},
                Actors = new []
                {
                    "Jason Sudeikis, Josh Gad, Danny McBride, Peter Dinklage, Rachel Bloom, Bill Hader, Awkwafina, Sterling K. Brown"
                },
                IsReleased = true
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Horrible Histories: The Movie - Rotten Romans",
                BriefDescription =
                    "Atti, a smart and quick-witted Roman teenager, manages to upset Emperor Nero with one of his schemes. For punishment, Atti is sent to work in a cold and wet Britain where he also meets the Celts.",
                DetailedDescription =
                    "Who are the Celts? What have the Romans ever done for us? And why is Emperor Nero dousing himself in horse wee? Friends, Romans, Celts... Lend us your ears. The all-conquering Romans rule the civilised world – and that includes 'the stain' that is Britain. While the young Emperor Nero must battle his scheming mother Agrippina for ultimate power, Celt queen Boudicca gathers an army in Britain to repel the rotten Romans. Mixed up in this battle for liberation are the teenage Atti, a reluctant Roman soldier, and Orla, a young Celt with dreams of becoming a warrior like Boudicca. Will they fall on opposite sides or forge a friendship in the chaos of Celtic-inspired rebellion?",
                ImagePath = "/CurrentFilmImages/HorribleHistories.jpg",
                Year = "2019",
                Length = "1h 32m",
                Classification = "ClassPG",
                Directors = new [] {"Dominic Brigstocke"},
                Genres = new [] {"Comedy", "Family", "History"},
                Actors = new []
                {
                    "Sebastian Croft, Emilia Jones, Nick Frost, Craig Roberts, Kim Cattrall, Kate Nash, Rupert Graves, Alex Macqueen, Sir Derek Jacobi, Alexander Armstrong, Lee Mack, Warwick Davis, Lucy Montgomery"
                },
                IsReleased = true
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Stuber",
                BriefDescription =
                    "When a mild-mannered Uber driver named Stu Kumail Nanjiani picks up a passenger Dave Bautista who turns out to be a cop hot on the trail of a brutal killer, he's thrust into a harrowing ordeal.",
                DetailedDescription =
                    "A mild-mannered Uber driver named Stu picks up a grizzled detective who is hot on the trail of a sadistic, bloodthirsty terrorist and finds himself thrust into a harrowing ordeal where he has to keep his wits, himself unharmed, and work with his passenger while maintaining his high-class rating.",
                ImagePath = "/CurrentFilmImages/Stuber.jpg",
                Year = "2019",
                Length = "1h 33m",
                Classification = "Class15",
                Directors = new [] {"Michael Dowse"},
                Genres = new [] {"Action", "Comedy", "Crime", "Thriller"},
                Actors = new []
                {
                    "Kumail Nanjiani, Dave Bautista, Iko Uwais, Natalie Morales, Betty Gilpin, Jimmy Tatro, Mira Sorvino, Karen Gillan"
                },
                IsReleased = true
            });
        }

        private void InitialiseUpcomingFilms1()
        {
            _filmRepo.AddFilm(new Film
            {
                Title = "Goodboys",
                BriefDescription =
                    "Three sixth-graders try to impress girls and upperclassmen by skipping school and attending parties.",
                DetailedDescription =
                    "Invited to his first kissing party, 12-year-old Max asks his best friends Lucas and Thor for some much-needed help on how to pucker up. When they hit a dead end, Max decides to use his father's drone to spy on the teenage girls next door. When the boys lose the drone, they skip school and hatch a plan to retrieve it before Max's dad can figure out what happened.",
                ImagePath = "/UpcomingFilmsImages/Goodboys.jpg",
                Year = "2019",
                Length = "1h 40m",
                Classification = "Class15",
                Directors = new [] {"Lee Eisenberg"},
                Genres = new [] {"Adventure", "Comedy"},
                Actors = new []
                {
                    "Jacob Tremblay", "Brady Noon, Keith L. Williams", "Will Forte", "Molly Gordon",
                    "Midori Francis"
                },
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Zombieland: Double Tap",
                BriefDescription =
                    "Zombie slayers Tallahassee, Columbus, Wichita and Little Rock square off against the newly evolved undead.",
                DetailedDescription =
                    "Columbus, Tallahasse, Wichita, and Little Rock move to the American heartland as they face off against evolved zombies, fellow survivors, and the growing pains of the snarky makeshift family.",
                ImagePath = "/UpcomingFilmsImages/Zombieland2.png",
                Year = "2019",
                Length = "2h 30m",
                Classification = "Class15",
                Directors = new [] {"Ruben Fleischer"},
                Genres = new [] {"Action", "Comedy", "Horror"},
                Actors = new []
                {
                    "Abigail Breslin", "Zoey Deutch", "Emma Stone", "Woody Harrelson", "Avan Jogia",
                    "Rosario Dawson", "Jesse Eisenberg", "Bill Murray"
                },
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Gemini Man",
                BriefDescription = "An over-the-hill hitman faces off against a younger clone of himself.",
                DetailedDescription =
                    "Gemini Man is an innovative action-thriller starring Will Smith as Henry Brogan, an elite assassin, who is suddenly targeted and pursued by a mysterious young operative that seemingly can predict his every move.",
                ImagePath = "/UpcomingFilmsImages/GeminiMan.png",
                Year = "2019",
                Length = "2h 29m",
                Classification = "Class15",
                Directors = new [] {"Ang Lee"},
                Genres = new [] {"Action", "Drama", "Sci-fi"},
                Actors = new []
                {
                    "Will Smith", "Mary Elizabeth Winstead", "Clive Owen", "Benedict Wong", "Theodora Miranne",
                    "Douglas Hodge", "Ralph Brown", "Linda Emond"
                },
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film
            {
                Title = "Jumanji: The Next Level",
                BriefDescription =
                    "Everything you know about Jumanji is about to change as the gang set off for another adventure. Get ready for a crazy ride!",
                DetailedDescription =
                    "A team of friends return to Jumanji to rescue one of their own but discover that nothing is as they expect. The players need to brave parts unknown, from arid deserts to snowy mountains, in order to escape the world's most dangerous game.",
                ImagePath = "/UpcomingFilmsImages/Jumanji.jpg",
                Year = "2019",
                Length = "2h 49m",
                Classification = "Class12",
                Directors = new [] {"Jake Kasdan"},
                Genres = new [] {"Action", "Adventure", "Comedy"},
                Actors = new []
                {
                    "Dwayne Johnson", "Awkwafina", "Karen Gillan", "Ashley Scott", "Jack Black", "Kevin Hart",
                    "Madison Iseman", "Colin Hanks", "Danny DeVito", "Danny Glover"
                },
                IsReleased = false
            });
            IntitialiseUpcomingFilms2();
        }

        private void IntitialiseUpcomingFilms2()
        {
            _filmRepo.AddFilm(new Film(){
                Title = "IT Chapter Two",
                BriefDescription =
                    "Because every 27 years evil revisits the town of Derry, Maine, IT CHAPTER TWO brings the characters - who've long since gone their separate ways - back together as adults, nearly three decades after the events of the first film.",
                DetailedDescription =
                    "Twenty-seven years after the Losers Club defeated Pennywise Bill Skarsgård, it returns to terrorize the town of Derry once more. Now adults, the Losers have long since gone their separate ways. However, the kids are disappearing again, so Mike Isaiah Mustafa, the only one of the group to remain in their hometown, calls the others home. Damaged by the experiences of their past, they must each conquer their deepest fears to destroy Pennywise once and for all... putting them directly in the path of the clown that become deadlier than ever.",
                ImagePath = "/UpcomingFilmsImages/IT.jpg",
                Year = "2019",
                Length = "2h 45m",
                Classification = "Class18",
                Directors = new[] {"Andy Muschietti"},
                Genres = new[] {"Horror", "Thriller"},
                Actors = new[] {"Finn Wolfhard, Bill Skarsgård, Jessica Chastain"},
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film(){
                Title = "Angel Has Fallen",
                BriefDescription =
                    "Secret Service Agent Mike Banning is framed for the attempted assassination of the President and must evade his own agency and the FBI as he tries to uncover the real threat.",
                DetailedDescription =
                    "When there is an assassination attempt on US President Allan Trumbull Morgan Freeman, his trusted confidant, Secret Service Agent Mike Banning Gerard Butler, is wrongfully accused and taken into custody. After escaping from capture, he becomes a man on the run and must evade his own agency and outsmart the FBI in order to find the real threat to the President. Desperate to uncover the truth, Banning turns to unlikely allies to help clear his name, keep his family from harm and AddFilm the country from imminent danger.",
                ImagePath = "/UpcomingFilmsImages/angel.jpg",
                Year = "2019",
                Length = "2h 01m",
                Classification = "Class15",
                Directors = new[] {"Ric Roman Waugh"},
                Genres = new[] {"Action"},
                Actors = new[]
                {
                    "Gerard Butler, Morgan Freeman, Jada Pinkett Smith, Lance Reddick, Tim Blake Nelson, Piper Perabo, Nick Nolte, Danny Huston"
                },
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film()
            {
                Title = "Crawl",
                BriefDescription =
                    "A young woman, while attempting to AddFilm her father during a category 5 hurricane, finds herself trapped in a flooding house and must fight for her life against alligators.",
                DetailedDescription =
                    "When a massive hurricane hits her Florida hometown, Haley Kaya Scodelario ignores evacuation orders to search for her missing father Barry Pepper. Finding him gravely injured in the crawl space of their family home, the two become trapped by quickly encroaching floodwaters. As time runs out to escape the strengthening storm, Haley and her father discover that the rising water level is the least of their fears.",
                ImagePath = "/UpcomingFilmsImages/Crawl.jpg",
                Year = "2019",
                Length = "1h 27m",
                Classification = "Class15",
                Directors = new[] {"Alexandre Aja"},
                Genres = new[] {"Horror", "Thriller"},
                Actors = new[] {"Kaya Scodelario, Barry Pepper"},
                IsReleased = false
            });
            _filmRepo.AddFilm(new Film()
            {
                Title = "Maleficent: Mistress Of Evil",
                BriefDescription =
                    "Maleficent is back. Get ready for a whole new adventure with Maleficent and Aurora as they face new threats to the magical land of the Moors.",
                DetailedDescription =
                    "Maleficent and her goddaughter Aurora begin to question the complex family ties that bind them as they are pulled in different directions by impending nuptials, unexpected allies, and dark new forces at play.",
                ImagePath = "/UpcomingFilmsImages/Maleficent.jpg",
                Year = "2019",
                Length = "1h 40m",
                Classification = "Class12A",
                Directors = new[] {"Joachim Rřnning"},
                Genres = new[] {"Adventure", "Family", "Fantasy"},
                Actors = new[] {"Teresa Mahoney, Chiwetel Ejiofor, Angelina Jolie "},
                IsReleased = false
            });
        }

        private void InitialiseReviews()
        {
            _reviewHandler.DeleteAll();
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Goodboys").Id, "Coxy91", "3", "This film was okay, but the script was poor and the acting left a lot to be desired.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Goodboys").Id, "Andy", "4", "Really enjoyed this film, would definitely watch again.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Goodboys").Id, "Ian", "1", "This film was a load of shit.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Goodboys").Id, "Dale", "5", "Film of the year! This shit was Oscar Worthy.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Zombieland: Double Tap").Id, "Carl", "3", "Meh, it was alright, wont be watching again.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Gemini Man").Id, "Dale", "1", "This film was so rubbish it's unreal. Will Smith should be ashamed.");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Gemini Man").Id, "Arun", "5", "WHAT A FILM! ABSOLUTELY BLEW MY MIND");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Jumanji: The Next Level").Id, "Ben", "3", "It was okay but I HATE KEVIN HART. FIRE HIM NOW!");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("Jumanji: The Next Level").Id, "Carl", "5", "Masterpiece of a film");
            _reviewHandler.AddReview(_filmRepo.FindByTitle("The Current War").Id, "Ian", "5", "Wonderful superb, must see film.");
        }

        private void InitialiseComments()
        {
            _commentRepo.deleteAll();
            _commentRepo.AddComment(GetFirstReviewIdFromFilmTitle("Goodboys"), "Dale", "I completely agree");
            _commentRepo.AddComment(GetFirstReviewIdFromFilmTitle("Goodboys"), "Ian", "You're chatting shit mate.");
            _commentRepo.AddComment(GetFirstReviewIdFromFilmTitle("Goodboys"), "Andy", "Fuck you, you piece of shit.");
            _commentRepo.AddComment(GetFirstReviewIdFromFilmTitle("Zombieland: Double Tap"), "Carl", "Mate, that bit at the end blew my mind! Stunning");
        }

        private void InitialiseShowings()
        {
            _showRepo.DeleteAll();
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Standard", "15:00", "09/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Standard", "11:00", "10/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Standard", "14:00", "10/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Standard", "20:00", "11/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Dbox", "19:00", "09/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Dbox", "21:00", "09/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Dbox", "18:00", "10/08/2019");
            _showRepo.AddShowing(_filmRepo.FindByTitle("The Current War").Id, "Dbox", "18:00", "11/08/2019");
        }

        private string GetFirstReviewIdFromFilmTitle(string filmTitle)
        {
            return ((Review)(_reviewHandler.GetByFilmId(_filmRepo.FindByTitle(filmTitle).Id).Value.contentList[1])).Id;
        }
    }
}