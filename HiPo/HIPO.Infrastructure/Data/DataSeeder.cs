using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HIPO.Infrastructure;

public static class HipoDbSeeder
{
    public static async Task SeedHipoQuestionsAsync(HipoDbContext context)
    {
        if (await context.HipoAssessmentQuestions.AnyAsync())
            return; // Already seeded

        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "Questions.json");

        if (!File.Exists(jsonPath))
            throw new FileNotFoundException("Questions.json not found at path: " + jsonPath);

        var jsonData = await File.ReadAllTextAsync(jsonPath);

        var questions = JsonSerializer.Deserialize<List<HipoAssessmentQuestions>>(jsonData, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (questions == null || !questions.Any())
            return;

        foreach (var question in questions)
        {
            question.QuestionId = Guid.NewGuid(); // Assign new GUID
        }

        await context.HipoAssessmentQuestions.AddRangeAsync(questions);
        await context.SaveChangesAsync();
    }
    public static async Task SeedHipoAssessmentOptionsAsync(HipoDbContext context)
    {
        if (await context.HipoAssessmentOptions.AnyAsync())
            return; // Already seeded

        var options = new List<HipoAssessmentOptions>
    {
        new HipoAssessmentOptions { Id = Guid.NewGuid(), Option = "Interesting" },
        new HipoAssessmentOptions { Id = Guid.NewGuid(), Option = "Challenging" }
    };

        await context.HipoAssessmentOptions.AddRangeAsync(options);
        await context.SaveChangesAsync();
    }
}

// using HIPO.Infrastructure;
// using Microsoft.EntityFrameworkCore;
// using System.Text.Json;

// namespace HIPO.Infrastructure;

// public static class HipoDbSeeder
// {
//     public static async Task SeedHipoUsersAsync(HipoDbContext context)
//     {
//         if (await context.HipoUser.AnyAsync())
//             return; // Already seeded

//         var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "Questions.json");

//         if (!File.Exists(jsonPath))
//             throw new FileNotFoundException("hipousers.json not found at path: " + jsonPath);

//         var jsonData = await File.ReadAllTextAsync(jsonPath);

//         var users = JsonSerializer.Deserialize<List<HipoAssessmentQuestions>>(jsonData, new JsonSerializerOptions
//         {
//             PropertyNameCaseInsensitive = true
//         });

//         if (users == null || !users.Any())
//             return;

//         // Assign new GUIDs if missing
//         foreach (var user in users)
//         {
//             user.QuestionId = Guid.NewGuid();
//         }

//         await context.HipoUser.AddRangeAsync(users);
//         await context.SaveChangesAsync();
//     }
// }

