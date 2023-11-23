using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace StudentsList;

    public class StudentService
    {
        public async Task Add(string name, int score, ApplicationDbContext db)
        {
            var student = await db.Student.Where(s => s.Name == name).FirstOrDefaultAsync();
            if (student == null)
            {
                db.Student.Add(new Students() { Name = name, Score = score });
                await db.SaveChangesAsync();
            }
            Console.WriteLine("Student Was Added");
        }
    }

