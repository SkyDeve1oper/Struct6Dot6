using System.Linq;
using System.Xml.Linq;

namespace Struct6Dot6
{
    sealed public class XmlDoc
    {
        XDocument xDocument;

        public void Add(Student student, Lesson[] lessons, int count, Group group)
        {

            int id = CountOfElements();

            XElement Id = new XElement("Id", new XAttribute("id", ++id));
            Id.Add(new XElement("Surname", student.Surname));
            Id.Add(new XElement("Name", student.Name));
            Id.Add(new XElement("Patronymic", student.Patronymic));
            Id.Add(new XElement("Group", group.Title));

            int avg = 0;
            foreach (Lesson lesson in lessons)
            {
                avg += lesson.Point;
            }

            XElement Lessons = new XElement("Lessons", new XAttribute("avg", avg / 5.0));
            for (int i = 0; i < 5; i++)
            {
                XElement les = new XElement($"{lessons[i].Name}");
                les.Add(new XAttribute("Point", lessons[i].Point));
                Lessons.Add(les);
            }

            Id.Add(Lessons);
            xDocument.Root.Add(Id);
        }
        public void Save()
        {
            try
            {
                xDocument.Save("StudentsXml");
            }
            catch 
            {

            }
        }

        public int CountOfElements()
        {
            int count = 0;
            try
            {
                XElement lastElement = xDocument.Root.Elements().Last();
                count = int.Parse(lastElement.Attribute("id").Value);
            }
            catch 
            {
            }

            return count;
        }

        public XmlDoc()
        {
            xDocument = new XDocument();
            xDocument.Add(new XElement("Students"));
            xDocument.Save("StudentsXml");
        }
    }
}
