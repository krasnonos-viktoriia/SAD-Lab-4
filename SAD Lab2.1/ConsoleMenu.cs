using AutoMapper;
using BLL.Models;
using BLL.Services;
using DAL.Data;
using DAL.UnitOfWork;
using BLL.Models;
//using Domain.Entities;
//using Domain.Entities.Enums;


namespace SAD_Lab2._1
{
    // Клас ConsoleMenu керує взаємодією з користувачем через консольний інтерфейс.
    public class ConsoleMenu
    {
        // Сервіси для взаємодії з відповідними сутностями
        private readonly UserService _users;
        private readonly PlaceService _places;
        private readonly ReviewService _reviews;
        private readonly QuestionService _questions;
        private readonly VisitService _visits;
        private readonly MediaFileService _media;

        // Поточний користувач, який увійшов у систему
        //private User? _currentUser;
        private UserModel _currentUser;

        // Ініціалізує сервіси через патерн UnitOfWork для роботи з базою даних
        public ConsoleMenu(AppDbContext context, IMapper mapper)
        {
            var unitOfWork = new UnitOfWork(context);

            _users = new UserService(unitOfWork, mapper);
            _places = new PlaceService(unitOfWork, mapper);
            _reviews = new ReviewService(unitOfWork, mapper);
            _questions = new QuestionService(unitOfWork, mapper);
            _visits = new VisitService(unitOfWork, mapper);
            _media = new MediaFileService(unitOfWork, mapper);
        }


        // Призупиняє виконання до натискання клавіші, потім очищає консоль
        private void Pause()
        {
            Console.ReadKey();
            Console.Clear();
        }

        // Основний цикл, що дозволяє обирати режим користувача або менеджера.
        public async Task RunAsync()
        {
            while (true)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Clear();
                Console.WriteLine("ОБЕРІТЬ РЕЖИМ РОБОТИ:");
                Console.WriteLine("1. Увійти як користувач");
                Console.WriteLine("2. Увійти як менеджер");
                Console.WriteLine("3. Вийти з програми");
                Console.Write("Ваш вибір: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": await HandleUserLogin(); break;
                    case "2": await HandleManagerLogin(); break;
                    case "3":
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть Enter для повтору.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Обробка входу користувача, з можливістю створення нового.
        private async Task HandleUserLogin()
        {
            Console.Clear();
            Console.Write("Введіть вашу email-адресу: ");
            var email = Console.ReadLine();

            _currentUser = (await _users.GetAllAsync())
                .FirstOrDefault(u => u.Email == email);

            if (_currentUser == null)
            {
                Console.Write("Введіть ваше ім’я: ");
                var name = Console.ReadLine();

                var newUser = new UserModel
                {
                    Name = name!,
                    Email = email!,
                    Role = UserModel.RoleEnum.Regular 
                };

                await _users.AddAsync(newUser);

                _currentUser = (await _users.GetAllAsync())
                    .FirstOrDefault(u => u.Email == email)!;

                Console.WriteLine("Користувача створено.");
            }

            Console.WriteLine($"Вітаємо, {_currentUser.Name}!");
            Pause();
            await UserMenu();
        }

        // Обробка входу менеджера, обмеженого конкретною email-адресою.
        private async Task HandleManagerLogin()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("Введіть email менеджера: ");
                var email = Console.ReadLine();

                if (email?.ToLower() != "manager@example.com")
                {
                    Console.WriteLine("Невірний email менеджера. Спробуйте ще раз.");
                    Pause();
                    continue;
                }

                _currentUser = (await _users.GetAllAsync())
                    .FirstOrDefault(u => u.Email == email);

                if (_currentUser == null)
                {
                    _currentUser = new UserModel
                    {
                        Name = "Admin",
                        Email = email,
                        Role = UserModel.RoleEnum.Manager
                    };

                    await _users.AddAsync(_currentUser);
                    Console.WriteLine("Менеджера створено.");
                }

                Console.WriteLine("Вхід як менеджер успішний.");
                Pause();
                await ManagerMenu();
                break;
            }
        }

        // Меню користувача
        private async Task UserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== КОРИСТУВАЧ ===");
                Console.WriteLine("1. Переглянути всі місця");
                Console.WriteLine("2. Переглянути місця, де я побував");
                Console.WriteLine("3. Додати місце, де я побував");
                Console.WriteLine("4. Переглянути питання та відповіді");
                Console.WriteLine("5. Дати відповідь на питання");
                Console.WriteLine("6. Переглянути відгуки");
                Console.WriteLine("7. Додати відгук");
                Console.WriteLine("8. Переглянути додаткову інформацію");
                Console.WriteLine("9. Додати додаткову інформацію");
                Console.WriteLine("0. Вийти");

                Console.Write("Ваш вибір: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": await ShowPlaces(); break;
                    case "2": await ShowVisitedPlaces(); break;
                    case "3": await AddVisit(); break;
                    case "4": await ShowQuestions(); break;
                    case "5": await AnswerQuestion(); break;
                    case "6": await ShowReviews(); break;
                    case "7": await AddReview(); break;
                    case "8": await ShowAllMedia(); break;
                    case "9": await AddMedia(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        Pause();
                        break;
                }
            }
        }

        // Меню менеджера
        private async Task ManagerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЕДЖЕР ===");
                Console.WriteLine("1. Переглянути всі місця");
                Console.WriteLine("2. Додати місце");
                Console.WriteLine("3. Видалити місце");
                Console.WriteLine("4. Переглянути всі відгуки");
                Console.WriteLine("5. Видалити відгук");
                Console.WriteLine("6. Переглянути всі питання");
                Console.WriteLine("7. Додати питання");
                Console.WriteLine("8. Видалити питання");
                Console.WriteLine("9. Переглянути додаткову інформацію");
                Console.WriteLine("10. Видалити додаткову інформацію");
                Console.WriteLine("0. Вийти");

                Console.Write("Ваш вибір: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": await ShowPlaces(); break;
                    case "2": await AddPlace(); break;
                    case "3": await DeletePlace(); break;
                    case "4": await ShowReviews(); break;
                    case "5": await DeleteReview(); break;
                    case "6": await ShowQuestions(); break;
                    case "7": await AddQuestion(); break;
                    case "8": await DeleteQuestion(); break;
                    case "9": await ShowAllMedia(); break;
                    case "10": await DeleteMedia(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        Pause();
                        break;
                }
            }
        }

        // Виведення всіх місць з бази.
        private async Task ShowPlaces()
        {
            Console.Clear();
            var places = await _places.GetAllAsync();
            if (!places.Any())
            {
                Console.WriteLine("Наразі немає жодного місця.");
                Pause();
                return;
            }

            Console.WriteLine("Список місць:");
            foreach (var place in places)
            {
                Console.WriteLine($"[{place.Id}] {place.Name}");
                Console.WriteLine($"Адреса: {place.Address}");
                Console.WriteLine($"Опис: {place.Description}\n");
            }
            Pause();
        }


        // Додавання нового місця.
        private async Task AddPlace()
        {
            Console.Clear();
            Console.WriteLine("Додавання нового місця:");

            Console.Write("Назва місця: ");
            var name = Console.ReadLine();
            Console.Write("Адреса: ");
            var address = Console.ReadLine();
            Console.Write("Опис: ");
            var desc = Console.ReadLine();

            await _places.AddAsync(new PlaceModel
            {
                Name = name!,
                Address = address!,
                Description = desc!
            });

            Console.WriteLine("Місце додано успішно.");
            Pause();
        }


        // Видалення місця за ID.
        private async Task DeletePlace()
        {
            Console.Clear();
            Console.WriteLine("Видалення місця:");

            Console.Write("Введіть ID місця для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _places.DeleteAsync(id);
                Console.WriteLine("Місце видалено (якщо існувало).");
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }


        // Виведення відвіданих користувачем місць.
        private async Task ShowVisitedPlaces()
        {
            Console.Clear();
            var visits = await _visits.GetByUserIdWithIncludesAsync(_currentUser!.Id);
            if (!visits.Any())
            {
                Console.WriteLine("Ви ще не відвідали жодного місця.");
                Pause();
                return;
            }

            Console.WriteLine("Місця, які ви відвідали:");
            foreach (var visit in visits)
            {
                Console.WriteLine($"{visit.VisitDate:yyyy-MM-dd} — {visit.Place.Name}");
            }
            Pause();
        }

        // Додавання нового запису про відвідування.
        private async Task AddVisit()
        {
            Console.Clear();
            Console.WriteLine("Додавання відвідування:");

            Console.Write("Введіть ID місця, яке ви відвідали: ");
            if (int.TryParse(Console.ReadLine(), out int placeId))
            {
                var visit = new VisitModel
                {
                    UserId = _currentUser!.Id,
                    PlaceId = placeId,
                    VisitDate = DateTime.Now
                };

                await _visits.AddAsync(visit);
                Console.WriteLine("Відвідування додано.");
            }
            else
            {
                Console.WriteLine("Некоректний ID місця.");
            }
            Pause();
        }

        // Виведення всіх відгуків.
        private async Task ShowReviews()
        {
            Console.Clear();
             var reviews = await _reviews.GetAllWithIncludesAsync();
            if (!reviews.Any())
            {
                Console.WriteLine("Немає відгуків.");
                Pause();
                return;
            }

            Console.WriteLine("Всі відгуки:");
            foreach (var review in reviews)
            {
                Console.WriteLine($"Місце: {review.Place.Name}");
                Console.WriteLine($"Користувач: {review.User.Name}");
                Console.WriteLine($"Оцінка: {review.Rating}");
                Console.WriteLine($"Коментар: {review.Comment}");
                Console.WriteLine($"Дата: {review.CreatedAt:yyyy-MM-dd}\n");
            }
            Pause();
        }

        // Додавання нового відгуку до місця.
        private async Task AddReview()
        {
            Console.Clear();
            Console.WriteLine("Додавання відгуку:");

            Console.Write("Введіть ID місця: ");
            if (int.TryParse(Console.ReadLine(), out int placeId))
            {
                Console.Write("Оцінка (1-5): ");
                if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 5)
                {
                    Console.Write("Коментар: ");
                    string comment = Console.ReadLine()!;

                    await _reviews.AddAsync(new ReviewModel
                    {
                        UserId = _currentUser!.Id,
                        PlaceId = placeId,
                        Rating = rating,
                        Comment = comment,
                        CreatedAt = DateTime.Now
                    });

                    Console.WriteLine("Відгук додано.");
                }
                else
                {
                    Console.WriteLine("Некоректна оцінка.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID місця.");
            }
            Pause();
        }

        // Видалення відгуку за ID.
        private async Task DeleteReview()
        {
            Console.Clear();
            Console.WriteLine("Видалення відгуку:");

            Console.Write("Введіть ID відгуку для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _reviews.DeleteAsync(id);
                Console.WriteLine("Відгук видалено (якщо існував).");
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }

        // Виводить усі медіафайли з бази
        private async Task ShowAllMedia()
        {
            Console.Clear();
            var places = await _places.GetAllAsync();
            var media = await _media.GetAllAsync();
            if (!media.Any())
            {
                Console.WriteLine("Медіафайли відсутні.");
                Pause();
                return;
            }

            Console.WriteLine("Усі медіафайли:");
            foreach (var f in media)
            {
                Console.WriteLine($"ID: {f.Id}, Тип: {f.FileType}, Шлях/URL: {f.FilePath}, Місце: [{f.PlaceId}]: {f.Place.Name}");
            }

            Pause();
        }


        // Додавання медіафайлу.
        private async Task AddMedia()
        {
            Console.Clear();
            Console.WriteLine("Додавання медіа-файлу:");

            Console.Write("Введіть ID місця: ");
            if (int.TryParse(Console.ReadLine(), out int placeId))
            {
                Console.WriteLine("Оберіть тип файлу:");
                Console.WriteLine("1 — Фото");
                Console.WriteLine("2 — Відео");
                Console.WriteLine("3 — Аудіо");
                Console.WriteLine("4 — Посилання");
                Console.WriteLine("5 — Інше");

                if (int.TryParse(Console.ReadLine(), out int fileTypeChoice) && fileTypeChoice >= 1 && fileTypeChoice <= 5)
                {
                    var fileType = (MediaFileModel.FileTypeEnum)(fileTypeChoice - 1);
                    Console.Write("Введіть шлях до файлу або URL: ");
                    var path = Console.ReadLine()!;

                    await _media.AddAsync(new MediaFileModel
                    {
                        PlaceId = placeId,
                        FilePath = path,
                        FileType = fileType
                    });

                    Console.WriteLine("Медіа додано.");
                }
                else
                {
                    Console.WriteLine("Некоректний вибір типу файлу.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID місця.");
            }
            Pause();
        }

        // Видаляє медіафайл за ID
        private async Task DeleteMedia()
        {
            Console.Clear();
            Console.Write("Введіть ID медіафайлу для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _media.DeleteAsync(id);
                Console.WriteLine("Медіафайл видалено (якщо існував).");
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }

        // Виведення всіх питань і відповідей.
        private async Task ShowQuestions()
        {
            Console.Clear();
            //var questions = await _questions.GetAllAsync();
            var questions = await _questions.GetAllWithIncludesAsync();
            if (!questions.Any())
            {
                Console.WriteLine("Немає питань.");
                Pause();
                return;
            }

            Console.WriteLine("Питання та відповіді:");
            foreach (var q in questions)
            {
                Console.WriteLine($"Місце: {q.Place.Name}");
                Console.WriteLine($"Питання: {q.Text}");
                Console.WriteLine($"Відповідь: {(string.IsNullOrWhiteSpace(q.Answer) ? "Ще немає відповіді" : q.Answer)}\n");
            }
            Pause();
        }

        // Додавання нового питання до місця.
        private async Task AddQuestion()
        {
            Console.Clear();
            Console.WriteLine("Додавання запитання:");

            Console.Write("Введіть ID місця: ");
            if (int.TryParse(Console.ReadLine(), out int placeId))
            {
                Console.Write("Ваше запитання: ");
                string questionText = Console.ReadLine()!;

                await _questions.AddAsync(new QuestionModel
                {
                    PlaceId = placeId,
                    UserId = _currentUser!.Id,
                    Text = questionText
                });

                Console.WriteLine("Питання додано.");
            }
            else
            {
                Console.WriteLine("Некоректний ID місця.");
            }
            Pause();
        }

        // Відповідь на одне з невідповіданих питань.
        private async Task AnswerQuestion()
        {
            Console.Clear();
            var questions = await _questions.GetAllAsync();
            var unanswered = questions.Where(q => string.IsNullOrWhiteSpace(q.Answer)).ToList();

            if (!unanswered.Any())
            {
                Console.WriteLine("Немає запитань без відповіді.");
                Pause();
                return;
            }

            Console.WriteLine("Запитання без відповіді:");
            foreach (var q in unanswered)
            {
                Console.WriteLine($"[{q.Id}] {q.Text} (місце: {q.Place.Name})");
            }

            Console.Write("\nВведіть ID запитання, на яке хочете відповісти: ");
            if (int.TryParse(Console.ReadLine(), out int questionId))
            {
                var question = unanswered.FirstOrDefault(q => q.Id == questionId);
                if (question != null)
                {
                    Console.Write("Ваша відповідь: ");
                    var answer = Console.ReadLine()!;
                    await _questions.AnswerQuestionAsync(question.Id, answer);
                    Console.WriteLine("Відповідь додано.");
                }
                else
                {
                    Console.WriteLine("Питання не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }

        // Видалення питання за ID.
        private async Task DeleteQuestion()
        {
            Console.Clear();
            Console.WriteLine("Видалення запитання:");

            Console.Write("Введіть ID питання для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                await _questions.DeleteAsync(id);
                Console.WriteLine("Питання видалено (якщо існувало).");
            }
            else
            {
                Console.WriteLine("Некоректний ID.");
            }
            Pause();
        }
    }
}