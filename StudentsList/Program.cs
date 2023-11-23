using Microsoft.EntityFrameworkCore;
using StudentsList;
using System.Data;

var db = new ApplicationDbContext();

while (true)
{
    
    Console.WriteLine("Please Provide A Command?");
    var command = Console.ReadLine();

    if (command == "Add")
    {
        Console.WriteLine("Please Write A Name?");
        var name = Console.ReadLine();
        Console.WriteLine("Please Write A Score?");
        var score = int.Parse(Console.ReadLine());
        var create = new StudentService();
        create.Add(name, score, db);
    }

    else if (command == "Show")
    {
        Console.WriteLine("Please Write A Minimum Score?");
        var score = int.Parse(Console.ReadLine());
        var higherstudent = await db.Student.Where(s => s.Score > score).ToListAsync();
        foreach (var i in higherstudent)
        {
            Console.WriteLine("Name:" + i.Name + "Score:" + i.Score);
        }
    }

    else if (command == "Delete")
    {
        Console.WriteLine("Please Write A Name");
        var name = Console.ReadLine();
        var student = await db.Student.Where(s => s.Name == name).FirstOrDefaultAsync();
        if (student != null)
        {
            db.Student.Remove(student);
            await db.SaveChangesAsync();
        }
    }

    else if (command == "SearchName")
    {
        Console.WriteLine("Please Write A Name");
        var name = Console.ReadLine();
        var students = await db.Student.Where(s => s.Name.Contains(name)).ToListAsync();
        foreach (var i in students)
        {
            Console.WriteLine("Name:" + i.Name);
        }
    }

    else if (command == "Edit")
    {
        Console.WriteLine("Please Enter Your Id");
        var id = int.Parse(Console.ReadLine());
        var student = await db.Student.Where(s => s.Id == id).FirstOrDefaultAsync();
        if (student == null)
        {
            Console.WriteLine("Not Found");
        }

        else
        {
            Console.WriteLine("Enter Your Name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter Your Score");
            var score = int.Parse(Console.ReadLine());
            student.Name = name;
            student.Score = score;
            await db.SaveChangesAsync();
        }
        Console.WriteLine("Student Was Edited");
    }

    else
    {
        Console.WriteLine("Error Please Write A Command Like Add, Show, Delete, SearchName And Edit");
    }
}
