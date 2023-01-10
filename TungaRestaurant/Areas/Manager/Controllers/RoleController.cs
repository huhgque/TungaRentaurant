using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TungaRestaurant.Areas.Manager.Controllers
{
	[Area("Manager")]
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> roleManager;
		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			this.roleManager = roleManager;
		}
		public IActionResult Index()
		{
			List<IdentityRole> roleList = roleManager.Roles.ToList();
			return View(roleList);
		}
		[HttpPost]
		public async Task<IActionResult> Create([Bind("Name")] IdentityRole identityRole)
		{
			identityRole.NormalizedName = roleManager.KeyNormalizer.NormalizeName(identityRole.Name);
			await roleManager.CreateAsync(identityRole);
			return RedirectToAction(nameof(Index));
		}
		
		public async Task<IActionResult> Delete(string id)
		{
			IdentityRole role = await roleManager.FindByIdAsync(id);
			if(role != null)
			{
				await roleManager.DeleteAsync(role);
			}
            return RedirectToAction(nameof(Index));
		}
	}
}
