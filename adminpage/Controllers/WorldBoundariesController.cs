#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adminpage.Models;
using Npgsql;
using System.Data;

namespace adminpage.Controllers
{
    public class WorldBoundariesController : Controller
    {
        private readonly map_htmlContext _context;
        private DataSet dataSet = null;
        public WorldBoundariesController(map_htmlContext context)
        {
            _context = context;
        }


        // GET: WorldBoundaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorldBoundaries.ToListAsync());
        }

        // GET: WorldBoundaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldBoundary = await _context.WorldBoundaries
                .FirstOrDefaultAsync(m => m.Gid == id);
            if (worldBoundary == null)
            {
                return NotFound();
            }

            return View(worldBoundary);
        }

        // GET: WorldBoundaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worldBoundary = await _context.WorldBoundaries.FindAsync(id);
            if (worldBoundary == null)
            {
                return NotFound();
            }
            return View(worldBoundary);
        }

        // POST: WorldBoundaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Gid,Status,ColorCode,Region,Iso3,Continent,Name,Iso31661,FrenchShor,Geom,FugitiveStatus,GeneralInfo,EntryDoc,RegDoc,Transport,Housing,Nutrition,Pets,Charity,AddInfo,Upddate")] WorldBoundary worldBoundary)
        {
            if (id != worldBoundary.Gid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                WorldBoundary objFromBase = await _context.WorldBoundaries.FindAsync(worldBoundary.Gid);

                //This will put all attributes of subnetsettings in objFromBase
                objFromBase.AddInfo = worldBoundary.AddInfo;
                objFromBase.Charity = worldBoundary.Charity;
                objFromBase.EntryDoc = worldBoundary.EntryDoc;
                objFromBase.FugitiveStatus = worldBoundary.FugitiveStatus;
                objFromBase.GeneralInfo = worldBoundary.GeneralInfo;
                objFromBase.Housing = worldBoundary.Housing;
                objFromBase.Name = worldBoundary.Name;
                objFromBase.Nutrition = worldBoundary.Nutrition;
                objFromBase.Pets = worldBoundary.Pets;
                objFromBase.RegDoc = worldBoundary.RegDoc;
                objFromBase.Status = worldBoundary.Status;
                objFromBase.Transport = worldBoundary.Transport;
                objFromBase.Upddate = worldBoundary.Upddate;
                try
                {
                    _context.Update(objFromBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldBoundaryExists(worldBoundary.Gid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(worldBoundary);
        }


        private bool WorldBoundaryExists(int id)
        {
            return _context.WorldBoundaries.Any(e => e.Gid == id);
        }

        public async Task<IActionResult> WriteJson()
        {
            NpgsqlConnection connection = Connect(GlobalVar.host, Convert.ToInt32(GlobalVar.port), GlobalVar.database, GlobalVar.user, GlobalVar.password);
            NpgsqlDataAdapter jsonDataAdapter = new NpgsqlDataAdapter(
            "select st_asgeojson(world_boundaries.*) from world_boundaries;", connection);
            new NpgsqlCommandBuilder(jsonDataAdapter);
            jsonDataAdapter.Fill(getDataSet(), "Json");
            DataTable dt = getDataSet().Tables["Json"];
            if (dt.Rows.Count > 0)
            {
                string str = "var json_Territories_2 = {\n"
                + "\"type\": \"FeatureCollection\",\n"
                + "\"name\": \"Territories_2\",\n"
                + "\"crs\": { \"type\": \"name\", \"properties\": { \"name\": \"urn:ogc:def:crs:OGC:1.3:CRS84\" } },\n"
                + "\"features\": [\n";
                foreach (DataRow dr in dt.Rows)
                    str += dr[0].ToString() + ",\n";
                str = str.Remove(str.Length - 2);
                str += "]\n}";

                System.IO.File.WriteAllText("wwwroot/Territories_2.js", str);
                byte[] fileBytes = System.IO.File.ReadAllBytes("wwwroot/Territories_2.js");
                string fileName = "Territories_2.js";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                
            }
            return RedirectToAction("Index");

        }

        public NpgsqlConnection Connect(string host, int port, string database,
 string user, string parol)
        {
            NpgsqlConnectionStringBuilder stringBuilder =
            new NpgsqlConnectionStringBuilder();
            stringBuilder.Host = host;
            stringBuilder.Port = port;
            stringBuilder.Username = user;
            stringBuilder.Password = parol;
            stringBuilder.Database = database;
            stringBuilder.Timeout = 30;
            NpgsqlConnection connection =
            new NpgsqlConnection(stringBuilder.ConnectionString);
            connection.Open();
            return connection;
        }
        private DataSet getDataSet()
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
                dataSet.Tables.Add("Json");
            }
            return dataSet;
        }

        public async Task<IActionResult> Logout()
        {
            GlobalVar.user = null;
            GlobalVar.database = null;
            GlobalVar.host = null;
            GlobalVar.password = null;
            GlobalVar.port = null;
            return RedirectToAction("Index", "Authorization");

        }
    }
}
