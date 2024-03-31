namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        [Test]
        public void AddTextBook()
        {
            TextBook textBook = new TextBook("History", "Balabanov", "Humanity");

            UniversityLibrary library = new UniversityLibrary();

            var actualResult = library.AddTextBookToLibrary(textBook);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Book: History - 1");
            sb.AppendLine($"Category: Humanity");
            sb.AppendLine($"Author: Balabanov");

            var expectedResult = sb.ToString().TrimEnd();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void InvertoryNumberIsCorrect()
        {
            TextBook textBook1 = new("Title1", "Author1", "Category1");
            TextBook textBook2 = new("Title2", "Author2", "Category2");
            TextBook textBook3 = new("Title3", "Author3", "Category3");

            UniversityLibrary library = new();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            int actualResult = textBook2.InventoryNumber;

            Assert.AreEqual(2, actualResult);
        }

        [Test]
        public void CatalogueReturnsCorrect()
        {
            TextBook textBook1 = new("Title1", "Author1", "Category1");
            TextBook textBook2 = new("Title2", "Author2", "Category2");
            TextBook textBook3 = new("Title3", "Author3", "Category3");

            UniversityLibrary library = new();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            int actualResult = library.Catalogue.Count;

            Assert.AreEqual(3, actualResult);   
        }

        [Test]
        public void LoantextBook()
        {
            TextBook textBook1 = new("Title1", "Author1", "Category1");
            TextBook textBook2 = new("Title2", "Author2", "Category2");
            TextBook textBook3 = new("Title3", "Author3", "Category3");

            UniversityLibrary library = new();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            string actualResult = library.LoanTextBook(1, "Name1");

            Assert.AreEqual("Title1 loaned to Name1.", actualResult);
        }

        [Test]
        public void LoantextBookHasntReturnedTheBook()
        {
            TextBook textBook1 = new("Title1", "Author1", "Category1");
            TextBook textBook2 = new("Title2", "Author2", "Category2");
            TextBook textBook3 = new("Title3", "Author3", "Category3");

            UniversityLibrary library = new();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            library.LoanTextBook(1, "Name1");
            string actualResult = library.LoanTextBook(1, "Name1");

            Assert.AreEqual("Name1 still hasn't returned Title1!", actualResult);
        }

        [Test]
        public void ReturnTextBook()
        {
            TextBook textBook1 = new("Title1", "Author1", "Category1");
            TextBook textBook2 = new("Title2", "Author2", "Category2");
            TextBook textBook3 = new("Title3", "Author3", "Category3");

            UniversityLibrary library = new();

            library.AddTextBookToLibrary(textBook1);
            library.AddTextBookToLibrary(textBook2);
            library.AddTextBookToLibrary(textBook3);

            library.LoanTextBook(1, "Name1");

            string actualResult = library.ReturnTextBook(1);

            Assert.AreEqual(string.Empty, textBook1.Holder);
            Assert.AreEqual("Title1 is returned to the library.", actualResult);
        }
    }
}