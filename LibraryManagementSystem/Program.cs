namespace LibraryManagementSystem;
internal class Program
{
    static List<Book> books = new List<Book>();
    static void Main(string[] args)
    {
        while (true) 
        {
            Console.WriteLine("\nLibrary Book Management System");
            Console.WriteLine("1. Add a Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Find a Book by ID");
            Console.WriteLine("4. Borrow a Book");
            Console.WriteLine("5. Return a Book");
            Console.WriteLine("6. Remove a Book");
            Console.WriteLine("7. Search by Title or Author");
            Console.WriteLine("8. View Available Books");
            Console.WriteLine("9. View Borrowed Books");
            Console.WriteLine("10. Count Available & Borrowed Books");
            Console.WriteLine("11. Exit");
            Console.Write("Enter your choice: ");

            if(int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1: AddBook(); 
                            break;
                    case 2: ViewAllBooks(); 
                            break;
                    case 3: FindBookById(); 
                            break;
                    case 4: BorrowBook(); 
                            break;
                    case 5: ReturnBook();
                            break;
                    case 6: RemoveBook(); 
                            break;
                    case 7: SearchBooks(); 
                            break;
                    case 8: ViewAvailableBooks(); 
                            break;
                    case 9: ViewBorrowedBooks(); 
                            break;
                    case 10: CountBooks();
                            break;
                    case 11:
                            return;   

                    default: 
                            Console.WriteLine("Invalid choice. Try again."); 
                            break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }          
    }

    static void AddBook()
    {
        Console.Write("\nEnter Book ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            if (books[i].BookId == id)
            {
                Console.WriteLine("Error: A book with this ID already exists.");
                return;
            }
        }

        Console.Write("Enter Title: ");
        string title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            return;
        }

        Console.Write("Enter Author: ");
        string author = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Author cannot be empty.");
            return;
        }

        var book = new Book(id, title, author);
        books.Add(book);
        Console.WriteLine("Book added successfully!");
    }

    static void ViewAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        Console.WriteLine("\nBooks:");

        foreach (var book in books)
        {
            PrintBookDetails(book);
        }
    }

    static void FindBookById()
    {
        Console.Write("\nEnter Book ID: ");
        if (int.TryParse(Console.ReadLine(),out int id))
        {
            foreach (var book in books)
            {
                if (book.BookId == id)
                {
                    PrintBookDetails(book);
                    return;
                }
            }
            Console.WriteLine("Book not found.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    static void BorrowBook()
    {
        Console.Write("\nEnter Book ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            foreach (var book in books)
            {
                if (book.BookId == id)
                {
                    if (book.IsAvailable)
                    {
                        book.IsAvailable = false;
                        Console.WriteLine($"Book \"{book.Title}\" has been borrowed."); 
                    }
                    else
                    {
                        Console.WriteLine("Book is already borrowed.");
                    }
                    return;
                }
            }
            Console.WriteLine("Book not found.");
        }
    }

    static void ReturnBook()
    {
        Console.Write("\nEnter Book ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            foreach (var book in books)
            {
                if (book.BookId == id)
                {
                    if (!book.IsAvailable)
                    {
                        book.IsAvailable = true;
                        Console.WriteLine($"Book \"{book.Title}\" has been returned.");
                    }
                    else
                    {
                        Console.WriteLine("Book is already available.");
                    }
                    return;
                }
            }
            Console.WriteLine("Book not found.");
        }
    }

    static void RemoveBook()
    {
        Console.Write("\nEnter Book ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == id)
                {
                    books.RemoveAt(i);
                    Console.WriteLine("Book removed successfully.");
                    return;
                }
            }
            Console.WriteLine("Book not found.");
        }
    }

    static void SearchBooks()
    {
        Console.Write("\nEnter title or author to search: ");
        string input = Console.ReadLine();
        bool found = false;

        foreach (var book in books)
        {
            if (book.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0 || book.Author.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                PrintBookDetails(book);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No matching books found.");
        }
    }

    static void ViewAvailableBooks()
    {
        Console.WriteLine("\nAvailable Books:");
        bool foundAvailable = false;

        foreach (var book in books)
        {
            if (book.IsAvailable)
            {
                PrintBookDetails(book);
                foundAvailable = true;
            }
        }

        if (!foundAvailable)
        {
            Console.WriteLine("No available books at the moment.");
        }
    }

    static void ViewBorrowedBooks()
    {
        Console.WriteLine("\nBorrowed Books:");
        bool foundBorrowed = false;

        foreach (var book in books)
        {
            if (!book.IsAvailable)
            {
                PrintBookDetails(book);
                foundBorrowed = true;
            }
        }

        if (!foundBorrowed)
        {
            Console.WriteLine("No borrowed books at the moment.");
        }
    }

    static void CountBooks ()
    {
        int availableCount = 0;
        int borrowedCount = 0;

        foreach (var book in books)
        {
            if (book.IsAvailable)
            {
                availableCount++;
            }
            else
            {
                borrowedCount++;
            }
        }
        Console.WriteLine($"Total available books: {availableCount}");
        Console.WriteLine($"Total borrowed books: {borrowedCount}");
    }

    static void PrintBookDetails(Book book)
    {
        Console.WriteLine($"ID: {book.BookId,-5} Title: {book.Title,-20} Author: {book.Author,-20} Available: {(book.IsAvailable ? "Yes" : "No"),-10}");
    }
}
