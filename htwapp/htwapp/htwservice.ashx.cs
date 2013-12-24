using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace htwapp
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für htwservice
    /// </summary>
    public class htwservice : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            context.Response.ContentType = "text/plain";

            String function = context.Request.QueryString["function"];


            if (!String.IsNullOrEmpty(function))
            {
                switch (function)
                {
                    case "getfach":
                        getfach(context);
                        break;
                    case "dozentenlistview":
                        getDozentlistview(context);
                        break;
                    case "getfachview":
                        getfachview(context);
                        break;
                    case "wahlfaecher":
                        getwahlfach(context);
                        break;

                }



            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void getDozentlistview(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            var dozenten = from f in db.Dozenten
                           orderby f.nachname ascending
                           select f;


            context.Response.Write("<br>");
            foreach (var tr in dozenten)
            {
                context.Response.Write("<li><a href=\"" + tr.profiladd + "\"> <img src=\"" + tr.profilbildadd + "\"><h2>" + tr.vorname + " " + tr.nachname + "</h2><p>" + tr.mail + "</p></a></li>");
            }
        }



        public void getfach(HttpContext context)
        {
            String studiengang = context.Request.QueryString["studiengang"];
            String semester = context.Request.QueryString["semester"];

            String tag = context.Request.QueryString["tag"];
            context.Response.ContentType = "text/plain";
            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            var fach = from f in db.Fach
                       where f.tag.Equals(tag) && f.studiensemster == Convert.ToInt32(semester) && f.Studiengaenge.bezeichnung.Equals(studiengang)
                       orderby f.von ascending
                       select f;


            foreach (var x in fach)
            {


                Boolean b = Convert.ToBoolean(x.wahlfach);
                if (!b)
                {
                    context.Response.Write("<li><a " + "id=\"" + x.Id + "\"" + "class=\"fachview\" href=\"" + "#viewfach" + "\" onClick=\"getfachview(" + x.Id + ")\"> <h2>" + x.bezeichnung + "</h2><p>" + "von: " + x.von + " bis: " + x.bis + "</p></a></li>");
                }
                else
                {
                    //wenns ein wahlfach ist dann schau nach ob man das dazu schreiben soll. --> query
                }

            }







            /*
            FachTableAdapter ta = new FachTableAdapter();
            htwdata.FachDataTable tbl = new htwdata.FachDataTable();
            if(tag.Equals("montag"))
                tbl = ta.GetMontag();
            if (tag.Equals("dienstag"))
                tbl = ta.GetDienstag();
            if (tag.Equals("mittwoch"))
                tbl = ta.getMittwoch();
            if (tag.Equals("donnerstag"))
                tbl = ta.GetDonnerstag();
            if (tag.Equals("freitag"))
                tbl = ta.GetFreitag();
            if (tag.Equals("samstag"))
                tbl = ta.GetSamstag();
                switch (tag)
                 {
                     case "MONTAG":

                        tbl = ta.GetMontag();
                         break;
                     case "DIENSTAG":
                         tbl = ta.GetDienstag();
                         break;
                     case "MITTWOCH":
                         break;
                     case "DONNERSTAG":
                         break;
            
                 } 
         
         
         
         
         
                          foreach (htwdata.FachRow tr in tbl)
                 {
                     context.Response.Write("<li><a " + "id=\"" + tr.Id + "\"" + "class=\"fachview\" href=\"" + "#viewfach" + "\" onClick=\"getfachview("+ tr.Id +")\"> <h2>" + tr.bezeichnung + "</h2><p>" + "von: " + tr.von + " bis: " + tr.bis + "</p></a></li>");
                 }*/



        }


        public void getfachview(HttpContext context)
        {
            //fachview zurueckgeben
            //wird die seite fuer die view generiert
            String fachid = context.Request.QueryString["fach"];


            context.Response.ContentType = "text/plain";
            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            var fach = from f in db.Fach
                       where f.Id == Convert.ToInt32(fachid)
                       select f;

            foreach (var fachdozent in fach)
            {
                //inhalt bereitstellen
                context.Response.Write("<h2>" + fachdozent.Dozenten.vorname + "</h2>");
                context.Response.Write("Raum: " + fachdozent.raum + "</h2>");
            }

            /*      <br>
                  Raum: <strong id="#raum">1234</strong>
                  <br>
                  Uhrzeit: <strong>8:15 - 9:45</strong>
                  <br>
                  Dozent: <strong>Wieker, Horst</strong>
                  <br>
                  Modulbeschreibung: <strong>bla bla blubb</strong>
                  <br><br>
                  Dozent und Raum können später angeklickt werden um weitere Informationen zu erhalten*/
        }



        public void getwahlfach(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            var fach = from f in db.Fach
                       where f.wahlfach == true
                       select f;
            context.Response.Write("<fieldset data-role=\"controlgroup\">");
            context.Response.Write("<legend>Wahlpflichtfächer</legend>");
            context.Response.Write("<br><br>");
            foreach (var wahlfach in fach)
            {
                //inhalt bereitstellen

                context.Response.Write("<input type=\"checkbox\" name=\"" + wahlfach.Id + "\" id=\"" + wahlfach.Id + "\" class=\"custom\" />");
                context.Response.Write("<label for=\"" + wahlfach.Id + "\">" + wahlfach.bezeichnung + "</label>");

            }




            /* 
            

             <input type="checkbox" name="checkbox-2a" id="checkbox-2a" class="custom" />
             <label for="checkbox-2a">Einführung in W-Lan</label>

             <input type="checkbox" name="checkbox-3a" id="checkbox-3a" class="custom" />
             <label for="checkbox-3a">Irgendwas anderes</label>

             <input type="checkbox" name="checkbox-4a" id="checkbox-4a" class="custom" />
             <label for="checkbox-4a">Langweilig...</label>
         </fieldset>
         */
        }
    
    }
}