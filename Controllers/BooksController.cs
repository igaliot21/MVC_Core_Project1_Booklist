using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext context;
        
        public BooksController(AppDbContext Context)
        {
            this.context = Context;
        }

        public IActionResult Index(){
            return View(context.Books.ToList());
        }

        //Get : Book/Create
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book){
            if (ModelState.IsValid) {
                context.Add(book);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //Details : Books/Details/5 <- id
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            else {
                var book = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                else {
                    return View(book);
                }
            }
        }
        
        //Edit : Books/Edit/5 <- id
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var book = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(book);
                }
            }
        }
        //POST: Books/Edit/5 <- id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Book book) {
            if (id != book.Id)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                context.Books.Update(book);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //Delete : Books/Delete/5 <- id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var book = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(book);
                }
            }
        }

        //POST: Books/Delete/5 <- id
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var book = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (book == null){
                    return NotFound();
                }
                else{
                    context.Books.Remove(book);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        protected override void Dispose(bool disposing){
            if (disposing) context.Dispose();
        }
    }
}
