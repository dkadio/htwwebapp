using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace htwapp
{
    /// <summary>
    /// Der Handler der für die Clients die passenden Inhalte zur verfügung stellt
    /// </summary>
    public class htwservice : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            context.Response.ContentType = "text/plain";
            //abholen des QueryStrings um funktion zuordnen zu können
            String function = context.Request.QueryString["function"];

            //Nur wenn eine funktion angegeben ist
            if (!String.IsNullOrEmpty(function))
            {
                //auswahl der funktionen
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

        //gibt die liste der Dozenten zurueck
        public void getDozentlistview(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";

            //erstellen der Datenbankverbindung
            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            //gib alle dozenten aus und ordne sie alphabetisch nach vorname
            var dozenten = from f in db.Dozenten
                           orderby f.vorname ascending
                           select f;


            //bau das passende Template fuer die Ausgabe zusammen
            context.Response.Write("<br>");
            //fuer jeden tr in dozenten als dozent gib das ganze als list item zurueck
            foreach (var tr in dozenten)
            {
                context.Response.Write("<li><a href=\"" + tr.profiladd + "\"> <img src=\"" + tr.profilbildadd + "\"><h2>" + tr.vorname + " " + tr.nachname + "</h2><p>" + tr.mail + "</p></a></li>");
            }
        }


        //gibt das passende Fach zur anfrage zureueck
        public void getfach(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //hol die querys ab die fuer die auswahl noetig sind
            String studiengang = context.Request.QueryString["studiengang"];
            String semester = context.Request.QueryString["semester"];
            String wahlfaecher = context.Request.QueryString["wahlfaecher"];
            String tag = context.Request.QueryString["tag"];
            String[] wahlfachliste = null;

            //split wahlfacher into Array
            if (!String.IsNullOrEmpty(wahlfaecher))
            {
                //die wahlfachliste die unter den einstlelungen angewählt werden sind mi . voneinander getrennt und werden hier aufgeschlüsselt
                wahlfachliste = wahlfaecher.Split('.');
                setz das letzte auf -1
                for (int i = 0; i < wahlfachliste.Length; i++)
                {

                    if(wahlfachliste[i].Equals(",")){
                        wahlfachliste[i] = "-1";
                    }
                }

            }
            

            //datenbankverbindung
            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
            //query abfrage gib alle faehcer und wahlfaecher aus die benoetigt werden fuer das passende semester und studiengang
            var fach = from f in db.Fach
                       where f.tag.Equals(tag) && f.studiensemster == Convert.ToInt32(semester) && f.Studiengaenge.bezeichnung.Equals(studiengang)
                       orderby f.von ascending
                       select f;

   

            //bau das geruest fuer die faecher zusammen und gib sie zureck
            foreach (var x in fach)
            {

                Boolean b = Convert.ToBoolean(x.wahlfach);
                
                //wenn es kein wahlfach ist
                if (!b)
                {
                    context.Response.Write("<li><a " + "id=\"" + x.Id + "\"" + "class=\"fachview\" href=\"" + "#viewfach" + "\" onClick=\"getfachview(" + x.Id + ")\"> <h2>" + x.bezeichnung + "</h2><p>" + "von: " + x.von + " bis: " + x.bis + "</p></a></li>");
                }
                //wenns ein wahlfach ist dann schau nach ob man das dazu schreiben soll. --> query
               else
                {
                 
                    if (wahlfachliste != null) { 
                    for (int i = 0; i < wahlfachliste.Length; i++)
                    {
                        if (wahlfachliste[i].Equals(Convert.ToString(x.Id)))
                        {
                            context.Response.Write("<li><a " + "id=\"" + x.Id + "\"" + "class=\"fachview\" href=\"" + "#viewfach" + "\" onClick=\"getfachview(" + x.Id + ")\"> <h2>" + x.bezeichnung + "</h2><p>" + "von: " + x.von + " bis: " + x.bis + "</p></a></li>");

                        }
                    }
                   }
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


        //gibt die anzeige fuer die faecher aus
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

            //das muesste noch huebsch gemacht werden
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



        //Gibt die wahlfaeher fuer die einstellungen zurueck
        public void getwahlfach(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //db connection
            HtwDataClassesDataContext db = new HtwDataClassesDataContext();
         //abfrage nach wahlfaechern
            var fach = from f in db.Fach
                       where f.wahlfach == true
                       select f;
            //geruest fuer wahfaecher
            context.Response.Write("<fieldset data-role=\"controlgroup\">");
            context.Response.Write("<legend>Wahlpflichtfächer</legend>");
            context.Response.Write("<br><br>");
            foreach (var wahlfach in fach)
            {
                //inhalt bereitstellen

                context.Response.Write("<input type=\"checkbox\" name=\"" + wahlfach.Id + "\" id=\"" + wahlfach.Id + "\" class=\"custom checkbox\" >");
                context.Response.Write("<label for=\"" + wahlfach.Id + "\">" + wahlfach.bezeichnung + "</label>");

            }

        }
    
    }
}