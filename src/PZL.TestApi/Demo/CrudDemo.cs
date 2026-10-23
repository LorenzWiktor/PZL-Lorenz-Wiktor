using Microsoft.EntityFrameworkCore;
using PZL.TestApi.Data;
using PZL.TestApi.Domain;

namespace PZL.TestApi.Demo
{
    public static class CrudDemo
    {
        public static async Task RunAsync(AppDbContext context)
        {
            Project project = new Project
            {
                Name = "Projekt demonstracyjny",
                Description = "Sprawdzenie operacji EF Core."
            };

            context.Projects.Add(project);
            await context.SaveChangesAsync();
            Console.WriteLine($"Utworzono projekt o ID: {project.Id}.");

            TaskItem task = new TaskItem
            {
                Title = "Sprawdzenie CRUD",
                Status = TaskItemStatus.Todo,
                ProjectId = project.Id
            };

            TaskItem savedTask = new TaskItem
            {
                Title = "Sprawdzenie trwałości danych",
                Status = TaskItemStatus.Done,
                ProjectId = project.Id
            };

            context.Tasks.Add(task);
            context.Tasks.Add(savedTask);
            await context.SaveChangesAsync();

            Comment comment = new Comment
            {
                Content = "Komentarz demonstracyjny.",
                TaskItemId = task.Id
            };

            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            List<Project> activeProjects = await context.Projects
                .Where(project => !project.IsArchived)
                .OrderBy(project => project.Name)
                .AsNoTracking()
                .ToListAsync();

            Console.WriteLine("Aktywne projekty:");

            foreach (Project activeProject in activeProjects)
            {
                Console.WriteLine($"{activeProject.Id}: {activeProject.Name}");
            }

            List<TaskItem> todoTasks = await context.Tasks
                .Where(taskItem => taskItem.ProjectId == project.Id
                    && taskItem.Status == TaskItemStatus.Todo)
                .OrderBy(taskItem => taskItem.Title)
                .AsNoTracking()
                .ToListAsync();

            Console.WriteLine("Zadania Todo w utworzonym projekcie:");

            foreach (TaskItem todoTask in todoTasks)
            {
                Console.WriteLine($"{todoTask.Id}: {todoTask.Title}");
            }

            List<Comment> comments = await context.Comments
                .Where(taskComment => taskComment.TaskItemId == task.Id)
                .OrderBy(taskComment => taskComment.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            Console.WriteLine("Komentarze do zadania:");

            foreach (Comment taskComment in comments)
            {
                Console.WriteLine($"{taskComment.Id}: {taskComment.Content}");
            }

            task.Status = TaskItemStatus.Done;
            await context.SaveChangesAsync();

            TaskItem updatedTask = await context.Tasks
                .AsNoTracking()
                .SingleAsync(taskItem => taskItem.Id == task.Id);

            if (updatedTask.Status != TaskItemStatus.Done)
            {
                throw new InvalidOperationException("Nie udało się potwierdzić aktualizacji zadania.");
            }

            Console.WriteLine($"Status zadania po aktualizacji: {updatedTask.Status}.");

            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

            bool commentExists = await context.Comments
                .AnyAsync(taskComment => taskComment.Id == comment.Id);

            if (commentExists)
            {
                throw new InvalidOperationException("Komentarz nadal znajduje się w bazie.");
            }

            Console.WriteLine("Komentarz został usunięty.");

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();

            bool taskExists = await context.Tasks
                .AnyAsync(taskItem => taskItem.Id == task.Id);

            if (taskExists)
            {
                throw new InvalidOperationException("Zadanie nadal znajduje się w bazie.");
            }

            Console.WriteLine("Zadanie testujące CRUD zostało usunięte.");
            Console.WriteLine($"Projekt do ponownego odczytu: {project.Id}.");
            Console.WriteLine($"Pozostawione zadanie: {savedTask.Id}.");
        }

        public static async Task ReadAsync(AppDbContext context, int projectId)
        {
            Project? project = await context.Projects
                .Include(project => project.Tasks)
                .AsNoTracking()
                .FirstOrDefaultAsync(project => project.Id == projectId);

            if (project == null)
            {
                Console.WriteLine($"Nie znaleziono projektu o ID: {projectId}.");
                return;
            }

            Console.WriteLine($"Projekt {project.Id}: {project.Name}");
            Console.WriteLine("Zapisane zadania:");

            foreach (TaskItem task in project.Tasks)
            {
                Console.WriteLine($"{task.Id}: {task.Title}, status: {task.Status}");
            }
        }
    }
}

