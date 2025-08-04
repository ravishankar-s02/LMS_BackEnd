using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using AutoMapper;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;
using LMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LMS.Services
{
    public class GooseMenuService : IGooseMenuService
    {
        private readonly IDbConnection _db;

        public GooseMenuService(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<Dictionary<string, List<GooseMasterMenu>>> GetGooseMenuAsync()
        {
            var data = await _db.QueryAsync<GooseMenuRawModel>(
                "SS_GetGooseMenu_Hierarchical_JSON_SP",
                commandType: CommandType.StoredProcedure);

            var masterMenus = data
                .GroupBy(m => m.MenuMasterPK)
                .Select(g => new GooseMasterMenu
                {
                    LinkTitle = g.First().MasterLinkTitle,
                    LinkPath = g.First().MasterLinkPath,
                    Disabled = g.First().MasterIsDisabled,
                    Hidden = g.First().MasterIsHidden,
                    Children = g
                        .Where(c => c.ChildLinkCode != null)
                        .Select(c => new GooseChildMenu
                        {
                            LinkCode = c.ChildLinkCode,
                            LinkTitle = c.ChildLinkTitle,
                            LinkPath = c.ChildLinkPath,
                            Disabled = c.ChildIsDisabled ?? false,
                            Hidden = c.ChildIsHidden ?? false
                        }).ToList()
                }).ToList();

            return new Dictionary<string, List<GooseMasterMenu>>
            {
                { "SIMPLESOLVE", masterMenus }
            };
        }
    }
}