using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Systemconstants
/// </summary>
public class Systemconstants
{
    public Systemconstants()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public const string key= "b1a76556447c964f657efd81906f51c5";
    public const int ROLE_NONE = -1;
    public const int ROLE_NOTALLOW = 0;
    public const int ROLE_ALLOW = 1;

    public const string domain = "/";
    public const string domainname = "/";
    public const string VersionCSS = "1.0.9";
    public const string VersionJS = "1.0.9";

    public const string English = "en";
    public const string Francais = "fr";
    public const string Italiano = "it";
    public const string Espanol = "es";
    public const string Deutsch = "de";

    public const string Sales = "1";
    public const string CS = "2";
    public const string Director = "3";

    public const int INDEX = 1;
    public const int TOUR = 2;
    public const int DESTINATION = 3;
    public const int HOTEL = 4;

    public const int Testimonial = 1;
    public const int FeedBack = 2;

    public const int VIETNAM = 1;
    public const int CAMBODIA = 2;
    public const int LAOS = 3;
    public const int MYANMAR = 4;
    public const int THAILAND = 5;

    public const int Page_Home = 1;
    public const int Page_Blog = 2;
    public const int Page_OurGuides = 3;
    public const int Page_VietnamstayTeam = 4;
    public const int Page_News = 5;
    public const int Page_SearchTour = 6;
    public const int Page_Testimonial = 7;
    public const int Page_CustomizeRQ = 8;
    public const int Page_TravelGuide = 9;
    public const int Page_BestSellingTours = 10;
    public const int Page_ContactUs = 11;
    public const int Page_About = 12;

    private static Random random = new Random();
    public static string Version(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public enum Actions
    {
        visit,
        book
    }

    public enum GennderEn
    {
        Mr,
        Mrs,
        Miss
    }

    public enum GennderFr
    {
        M,
        Mme,
        Mlle
    }

    public enum GennderIt
    {
        Signore,
        Signora,
        Signorina
    }

    public enum GennderDe
    {
        Frau,
        Herr
    }

    public enum GennderEs
    {
        Sr,
        Sra,
        Srta
    }
    public enum GenderEn
    {
        Male,
        Female
    }
    public enum GenderFr
    {
        Homme,
        Femme
    }
    public enum GenderIt
    {
        Maschile,
        Femminile
    }
    public enum GenderDe
    {
        Männlich,
        Weiblich
    }
    public enum GenderEs
    {
        Hombre,
        Mujer
    }
}
