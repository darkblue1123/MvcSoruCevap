using Microsoft.AspNetCore.Mvc;
using MvcSoruCevap.Models;

namespace MvcSoruCevap.Controllers
{
    public class QuestionController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Question
        public IActionResult Index()
        {
            var questions = _context.Questions
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
            return View(questions);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.CreatedAt = DateTime.UtcNow;
                _context.Questions.Add(question);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(question);
        }

        // GET: Question/Edit/5
        public IActionResult Edit(int id)
        {
            var question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(question);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(question);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(question);
        }

        // GET: Question/Delete/5
        public IActionResult Delete(int id)
        {
            var question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var question = _context.Questions.Find(id);
            _context.Questions.Remove(question);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
