using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;

namespace V308CMS.Common
{
    public class Emoticon
    {
        #region Static members.
        // ------------------------------------------------------------------

        /// <summary>
        /// Returns a collection of all available emoticons.
        /// </summary>
        public static Emoticon[] All
        {
            get
            {
                ArrayList result = new ArrayList();

                // --
                // Actually add the emoticons.

                #region ICON CU
                // MSN Messenger 4.5.
                //result.Add(new Emoticon("thumbs_up.gif", "(Y)", "Thumbs up"));
                //result.Add(new Emoticon("thumbs_down.gif", "(N)", "Thumbs down"));
                //result.Add(new Emoticon("beer_yum.gif", "(B)", "Beer"));
                //result.Add(new Emoticon("martini_shaken.gif", "(D)", "Martini Glass"));
                //result.Add(new Emoticon("girl_handsacrossamerica.gif", "(X)", "Girl"));
                //result.Add(new Emoticon("guy_handsacrossamerica.gif", "(Z)", "Boy"));
                //result.Add(new Emoticon("bat.gif", ":-[", "Bat"));
                //result.Add(new Emoticon("girl_hug.gif", "(})", "Hug right"));
                //result.Add(new Emoticon("dude_hug.gif", "({)", "Hug left"));
                //result.Add(new Emoticon("regular_smile.gif", ":-)", "smiley"));
                //result.Add(new Emoticon("teeth_smile.gif", ":-D", "Theeth smiley"));
                //result.Add(new Emoticon("omg_smile.gif", ":-O", "OMG smiley"));
                //result.Add(new Emoticon("tounge_smile.gif", ":-P", "Tounge smiley"));
                //result.Add(new Emoticon("wink_smile.gif", ";-)", "Wink smiley"));
                //result.Add(new Emoticon("sad_smile.gif", ":-)", "Sad smiley"));
                //result.Add(new Emoticon("confused_smile.gif", ":-S", "Confused smiley"));
                //result.Add(new Emoticon("whatchutalkingabout_smile.gif", ":-|", "Serious smiley"));
                //result.Add(new Emoticon("cry_smile.gif", ":'(", "Crying smiley"));
                //result.Add(new Emoticon("cry_smile.gif", ":-(", "Crying smiley"));
                //result.Add(new Emoticon("embaressed_smile.gif", ":$", "Embaressed smiley"));
                //result.Add(new Emoticon("shades_smile.gif", "(H)", "smiley with shades"));
                //result.Add(new Emoticon("angry_smile.gif", ":-@", "Angry smiley"));
                //result.Add(new Emoticon("angel_smile.gif", "(A)", "Angel smiley"));
                //result.Add(new Emoticon("devil_smile.gif", "(6)", "Devil smiley"));
                //result.Add(new Emoticon("heart.gif", "(L)", "Red heart"));
                //result.Add(new Emoticon("broken_heart.gif", "(U)", "Broken heart"));
                //result.Add(new Emoticon("kiss.gif", "(K)", "Red lips"));
                //result.Add(new Emoticon("present.gif", "(G)", "Present"));
                //result.Add(new Emoticon("rose.gif", "(F)", "Red rose"));
                //result.Add(new Emoticon("wilted_rose.gif", "(W)", "Wilted rose"));
                //result.Add(new Emoticon("camera.gif", "(P)", "Camera"));
                //result.Add(new Emoticon("film.gif", "(~)", "Film"));
                //result.Add(new Emoticon("phone.gif", "(T)", "Phone"));
                //result.Add(new Emoticon("kittykay.gif", "(@)", "Cat"));
                //result.Add(new Emoticon("bowwow.gif", "(&)", "Dog"));
                //result.Add(new Emoticon("coffee.gif", "(C)", "Coffee"));
                //result.Add(new Emoticon("lightbulb.gif", "(I)", "Light bulb"));
                //result.Add(new Emoticon("moon.gif", "(S)", "Moon"));
                //result.Add(new Emoticon("musical_note.gif", "(8)", "Musical note"));
                //result.Add(new Emoticon("envelope_open.gif", "(OE)", "Open envelope"));
                //result.Add(new Emoticon("cake.gif", "(^)", "Cake"));
                //result.Add(new Emoticon("clock.gif", "(O)", "Clock"));
                //result.Add(new Emoticon("rainbow.gif", "(R)", "Rainbow"));
                //result.Add(new Emoticon("sun.gif", "(#)", "Sun"));
                //result.Add(new Emoticon("questionmark.gif", "(?)", "Questionmark"));
                //result.Add(new Emoticon("hs.gif", "(%)", "Handcuff"));
                //result.Add(new Emoticon("teeth_smile.gif", ":@)", "Handcuff"));

                //// Some of MSN Messenger 6.																	 							
                //result.Add(new Emoticon("envelope.gif", "(e)", "Envelope"));
                //result.Add(new Emoticon("pizza.gif", "(pi)", "Pizza"));
                //result.Add(new Emoticon("soccer_ball.gif", "(so)", "Soccer ball"));
                //result.Add(new Emoticon("money.gif", "(mo)", "Money"));
                //result.Add(new Emoticon("island.gif", "(ip)", "Island"));
                //result.Add(new Emoticon("plane.gif", "(ap)", "Plane"));
                //result.Add(new Emoticon("auto.gif", "(au)", "Car"));
                //result.Add(new Emoticon("mobile_phone.gif", "(mp)", "Mobile phone"));
                //result.Add(new Emoticon("sheep.gif", "(bha)", "Sheep"));
                //result.Add(new Emoticon("snail.gif", "(sn)", "Snail"));
                //result.Add(new Emoticon("snail.gif", ":d", "Snail"));

                //// Some of my own.

                //result.Add(new Emoticon("uwe.gif", "(Uwe)", "The Uwe (<--clever!)", "http://www.magerquark.de"));
                //result.Add(new Emoticon("harald.gif", "(Harald)", "The Harald (www.geisselhart.de)", "http://www.geisselhart.de"));
                //result.Add(new Emoticon("johanna.gif", "(Johanna)", "The Johanna (www.kuhnijunior.de)", "http://www.kuhnijunior.de"));
                //result.Add(new Emoticon("andreas.gif", "(Andreas)", "The Andreas (www.kuhni.de)", "http://www.kuhni.de"));
                //result.Add(new Emoticon("klettern.gif", "(Climbing)", "Climbing"));
                //result.Add(new Emoticon("geburtstag.gif", "(Birthday cake)", "Birthday cake"));
                //result.Add(new Emoticon("t19.gif", "(:)", ""));
                //result.Add(new Emoticon("zeta-producer.gif", "(ZP)", "zeta producer", "http://www.zeta-producer.de"));
                //result.Add(new Emoticon("m3u.gif", "(WinAmp)", "Winamp"));
                //result.Add(new Emoticon("new.gif", "(new)", "New")); 
                #endregion
               
                result.Add(new Emoticon("1.gif", ":)", "Loud_Smiley"));
                result.Add(new Emoticon("2.gif", ":(", "Loud_Smiley"));
                result.Add(new Emoticon("3.gif", ";)", "Loud_Smiley"));
                result.Add(new Emoticon("4.gif", ":d", "Loud_Smiley"));
                result.Add(new Emoticon("5.gif", ";;)", "Loud_Smiley"));
                result.Add(new Emoticon("6.gif", ">:d<", "Loud_Smiley"));
                result.Add(new Emoticon("7.gif", ":-/", "Loud_Smiley"));
                result.Add(new Emoticon("8.gif", ":x", "Loud_Smiley"));
                result.Add(new Emoticon("9.gif", ":\">", "Loud_Smiley"));
                result.Add(new Emoticon("10.gif", ":p", "Loud_Smiley"));
                result.Add(new Emoticon("11.gif", ":-*", "Loud_Smiley"));
                result.Add(new Emoticon("12.gif", "=((", "Loud_Smiley"));
                result.Add(new Emoticon("13.gif", ":-o", "Loud_Smiley"));
                result.Add(new Emoticon("14.gif", "x(", "Loud_Smiley"));
                result.Add(new Emoticon("15.gif", ":>", "Loud_Smiley"));
                result.Add(new Emoticon("16.gif", "b-)", "Loud_Smiley"));
                result.Add(new Emoticon("17.gif", ":-s", "Loud_Smiley"));
                result.Add(new Emoticon("18.gif", "#:-s", "Loud_Smiley"));
                result.Add(new Emoticon("19.gif", ">:)", "Loud_Smiley"));
                result.Add(new Emoticon("20.gif", ":((", "Loud_Smiley"));
                result.Add(new Emoticon("21.gif", ":))", "Loud_Smiley"));
                result.Add(new Emoticon("22.gif", ":|", "Loud_Smiley"));
                result.Add(new Emoticon("23.gif", "/:)", "Loud_Smiley"));
                result.Add(new Emoticon("24.gif", "=))", "Loud_Smiley"));
                result.Add(new Emoticon("25.gif", "o:-)", "Loud_Smiley"));
                result.Add(new Emoticon("26.gif", ":-b", "Loud_Smiley"));
                result.Add(new Emoticon("27.gif", "=;", "Loud_Smiley"));
                result.Add(new Emoticon("28.gif", "i-)", "Loud_Smiley"));
                result.Add(new Emoticon("29.gif", "8-|", "Loud_Smiley"));
                result.Add(new Emoticon("30.gif", "l-)", "Loud_Smiley"));
                result.Add(new Emoticon("31.gif", ":-&", "Loud_Smiley"));
                result.Add(new Emoticon("32.gif", ":-$", "Loud_Smiley"));
                result.Add(new Emoticon("33.gif", "[-(", "Loud_Smiley"));
                result.Add(new Emoticon("34.gif", ":o)", "Loud_Smiley"));
                result.Add(new Emoticon("35.gif", "8-}", "Loud_Smiley"));
                result.Add(new Emoticon("36.gif", "<:-p", "Loud_Smiley"));
                result.Add(new Emoticon("37.gif", "(:|", "Loud_Smiley"));
                result.Add(new Emoticon("38.gif", "=p~", "Loud_Smiley"));
                result.Add(new Emoticon("39.gif", ":-?", "Loud_Smiley"));
                result.Add(new Emoticon("40.gif", "#-o", "Loud_Smiley"));
                result.Add(new Emoticon("41.gif", "=s>", "Loud_Smiley"));
                result.Add(new Emoticon("42.gif", ":-ss", "Loud_Smiley"));
                result.Add(new Emoticon("43.gif", "@-)", "Loud_Smiley"));
                result.Add(new Emoticon("44.gif", ":^o", "Loud_Smiley"));
                result.Add(new Emoticon("45.gif", ":-w", "Loud_Smiley"));
                result.Add(new Emoticon("46.gif", ":-<", "Loud_Smiley"));
                result.Add(new Emoticon("47.gif", ">:p", "Loud_Smiley"));
                result.Add(new Emoticon("48.gif", "<):)", "Loud_Smiley"));
                result.Add(new Emoticon("100.gif", ":)]", "Loud_Smiley"));
                result.Add(new Emoticon("101.gif", ":-c", "Loud_Smiley"));
                result.Add(new Emoticon("102.gif", "~x(", "Loud_Smiley"));
                result.Add(new Emoticon("103.gif", ":-h", "Loud_Smiley"));
                result.Add(new Emoticon("104.gif", ":-t", "Loud_Smiley"));
                result.Add(new Emoticon("105.gif", "8->", "Loud_Smiley"));
                result.Add(new Emoticon("106.gif", ":-w", "Loud_Smiley"));
                result.Add(new Emoticon("109.gif", "x_x", "Loud_Smiley"));
                result.Add(new Emoticon("111.gif", "\\m/", "Loud_Smiley"));
                result.Add(new Emoticon("112.gif", ":-q", "Loud_Smiley"));
                result.Add(new Emoticon("120.gif", "~^o^~", "Loud_Smiley"));
                result.Add(new Emoticon("122.gif", "[]---", "Loud_Smiley"));
                result.Add(new Emoticon("125.gif", "'+_+", "Loud_Smiley"));
                result.Add(new Emoticon("53.gif", "@};-", "Loud_Smiley"));
                result.Add(new Emoticon("59.gif", "8-x", "Loud_Smiley"));




                // --

                if (result.Count == 0)
                    return null;
                else
                    return (Emoticon[])result.ToArray(typeof(Emoticon));
            }
        }

        /// <summary>
        /// Returns a string with all emoticons replaced by their images.
        /// </summary>
        public static string Format(string input)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }
            else
            {
                StringBuilder result = new StringBuilder(input);

                Emoticon[] all = All;
                foreach (Emoticon emoticon in all)
                {
                    string a;
                    string a_;
                    int border;

                    // Decide whether a link is required.
                    if (emoticon.Url != null && emoticon.Url.Length > 0)
                    {
                        a = string.Format("<a href=\"{0}\">", emoticon.Url);
                        a_ = "</a>";
                        border = 1;
                    }
                    else
                    {
                        a = "";
                        a_ = "";
                        border = 0;
                    }

                    // Replace this emoticon.
                    string replacement =
                        string.Format(
                        "{0}<img style=\"display: inline !important;\" src=\"{1}\" alt=\"{2}\" align=\"AbsMiddle\" border=\"{3}\" />{4}",
                        a,
                        emoticon.VirtualPath,
                        HttpUtility.HtmlEncode(emoticon.Title),
                        border,
                        a_);

                    result = result.Replace(emoticon.Shortcut, replacement);
                }

                return result.ToString();
            }
        }

        // ------------------------------------------------------------------
        #endregion

        #region Constructors.
        // ------------------------------------------------------------------

        public Emoticon(Emoticon src)
        {
            Shortcut = src.Shortcut;
            Filename = src.Filename;
            Title = src.Title;
            Url = src.Url;

            Check();
        }

        public Emoticon(string filename, string shortcut)
        {
            Shortcut = shortcut;
            Filename = filename;

            Check();
        }

        public Emoticon(string filename, string shortcut, string title)
        {
            Shortcut = shortcut;
            Filename = filename;
            Title = title;

            Check();
        }

        public Emoticon(string filename, string shortcut, string title, string url)
        {
            Shortcut = shortcut;
            Filename = filename;
            Title = title;
            Url = url;

            Check();
        }

        // ------------------------------------------------------------------
        #endregion

        #region Properties.
        // ------------------------------------------------------------------

        /// <summary>
        /// The (case-sensitive!) string to be replaced with the emoticon.
        /// </summary>
        public string Shortcut = "";

        /// <summary>
        /// The filename (no path) of the emoticon.
        /// </summary>
        public string Filename = "";

        /// <summary>
        /// The optional tooltip of the emoticon.
        /// </summary>
        public string Title = "";

        /// <summary>
        /// The optional URL of the emoticon. If specified, the emoticon
        /// can be clicked.
        /// </summary>
        public string Url = "";

        // ------------------------------------------------------------------
        #endregion

        #region Internal helper.
        // ------------------------------------------------------------------

        /// <summary>
        /// Returns the complete virtual path.
        /// </summary>
        public string VirtualPath
        {
            get
            {
                string path = "~/Content/Emoticons/Yahoo/" + Filename;
                return ReplaceTilde(path);
            }
        }

        /// <summary>
        /// Get the root of the current web application.
        /// Expands a "~" character by the real path.
        /// </summary>
        private static string ReplaceTilde(string path)
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return path.Replace("~", "");
            else
                return path.Replace("~", HttpContext.Current.Request.ApplicationPath);
        }

        /// <summary>
        /// Do member-checking, whether it is valid.
        /// </summary>
        private void Check()
        {
            if (Shortcut == null || Shortcut.Length == 0)
                throw new ArgumentException("Emoticon.Shortcut must be non-empty", "Emoticon.Shortcut");
            if (Filename == null || Filename.Length == 0)
                throw new ArgumentException("Emoticon.Filename must be non-empty", "Emoticon.Filename");
        }

        // ------------------------------------------------------------------
        #endregion
    }
}