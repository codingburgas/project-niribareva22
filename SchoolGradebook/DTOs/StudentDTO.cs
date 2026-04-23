namespace SchoolGradebook.DTOs;

public class StudentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public double AverageGrade { get; set; } // Ще го ползваме за статистиката
}