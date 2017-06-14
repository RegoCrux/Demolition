using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;
using System.Collections.Generic;

public class XMLToObject
{
    public XMLToObject()
    {
    }


    //Creates an object from an XML string.
    //This is what we're doing for reading in the xml
    static List<User> DeserializeFromXML()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<User>));
        TextReader textReader = new StreamReader("Restaurant.xml");
        List<User> users = null;
        try
        {
            users = (List<User>)deserializer.Deserialize(textReader);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        textReader.Close();

        return users;
    }

    //Serializes the <i>Obj</i> to an XML string.
    static public void SerializeToXML(User movie)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(User));
        TextWriter textWriter = new StreamWriter(@"C:\testJen.xml");
        serializer.Serialize(textWriter, movie);
        textWriter.Close();
    }

    static void Main(string[] args)
    {
        /*
         * 
         * This is the opposite of what we're doing
         * 
        User jen = new User();
        jen.BaseWage = 1000;
        jen.Deductions = 1;
        jen.First = "Jennifer";
        jen.Last = "Goldbach";
        jen.JobTitle = "Tester";
        jen.TipsReceived = 50;

        SerializeToXML(jen);
         * */
        List<User> jen = DeserializeFromXML();

        /* users from the restaurant.xml files */
        foreach (User us in jen)
        {
            Console.WriteLine(us.FirstName + " " + us.LastName);
        }


    }
}