using Microsoft.AspNetCore.Mvc;
using MvcSoruCevap.Models;

namespace MvcSoruCevap.Controllers
{
    public class AnswerController : Controller
    {
        private readonly AppDbContext _context;

        public AnswerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Answer
        public IActionResult Index(int questionId)
        {
            var answers = _context.Answers
                .Where(a => a.QuestionId == questionId)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();

            ViewBag.Question = _context.Questions.Find(questionId);
            return View(answers);
        }

        // GET: Answer/Create
        public IActionResult Create(int questionId)
        {
            ViewBag.QuestionId = questionId;
            ViewBag.Users = _context.Users.ToList();
            return View();
        }

        // POST: Answer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.CreatedAt = DateTime.UtcNow;
                _context.Answers.Add(answer);
                _context.SaveChanges();
                return RedirectToAction("Index", new { questionId = answer.QuestionId });
            }
            ViewBag.QuestionId = answer.QuestionId;
            ViewBag.Users = _context.Users.ToList();
            return View(answer);
        }

        // GET: Answer/Edit/5
        public IActionResult Edit(int id)
        {
            var answer = _context.Answers.Find(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewBag.Users = _context.Users.ToList();
            return View(answer);
        }
        // POST: Answer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(answer);
                _context.SaveChanges();
                return RedirectToAction("Index", new { questionId = answer.QuestionId });
            }
            ViewBag.Users = _context.Users.ToList();
            return View(answer);
        }

        // GET: Answer/Delete/5
        public IActionResult Delete(int id)
        {
            var answer = _context.Answers.Find(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var answer = _context.Answers.Find(id);
            var questionId = answer.QuestionId;

            _context.Answers.Remove(answer);
            _context.SaveChanges();
            return RedirectToAction("Index", new { questionId });
        }


    }
}