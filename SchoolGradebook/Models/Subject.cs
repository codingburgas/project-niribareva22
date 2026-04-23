namespace SchoolGradebook.Models;

public class Subject : BaseEntity 
{
    public string Name { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
}