
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;



namespace WebApplication3.Areas.Admin.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public TeamController(AppDbContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        public IActionResult Index()
        {
            var teamSliders = _context.teams.ToList();
            return View(teamSliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team teamSlider)
        {
            if (!ModelState.IsValid) { return View(); }

            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            string filename = Guid.NewGuid() + teamSlider.ImgFile.FileName;


            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                teamSlider.ImgFile.CopyTo(stream);
                teamSlider.ImgUrl = filename;
            }
            _context.teams.Add(teamSlider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == 0) { return View(); }
            var user = _context.teams.FirstOrDefault(x => x.Id == id);

            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            FileInfo fileinfo = new FileInfo(path + user.ImgUrl);
            if (fileinfo.Exists)
            {
                fileinfo.Delete();
            }
            _context.teams.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var member = _context.teams.FirstOrDefault(x => x.Id == id);
            return View(member);
        }
        [HttpPost]
        public IActionResult Update(Team teamSlider)
        {
            if (!ModelState.IsValid) { return View(); }

            var member = _context.teams.FirstOrDefault(x => x.Id == teamSlider.Id);
            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            string filename = Guid.NewGuid() + teamSlider.ImgFile.FileName;
            FileInfo fileinfo = new FileInfo(path + member.ImgUrl);
            if (fileinfo.Exists)
            {
                fileinfo.Delete();
            }
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                teamSlider.ImgFile.CopyTo(stream);
                member.ImgUrl = filename;
            }
            member.FullName = teamSlider.FullName;
            member.Description = teamSlider.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
