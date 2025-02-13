using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class BranchRepository:IBranchRepository
    {
        private readonly DataContext _context;
        public BranchRepository(DataContext context)
        {
            _context = context;
        }
        public List<Branch> GetBranchesList()
        {
            return _context.Branches.ToList();
        }
        public Branch GetBranchById(int Id)
        {
            var branch = _context.Branches.Find(Id);
            if (branch != null)
            {
                return branch;
            }
            return null;
        }
        public void AddBranch(Branch branch)
        {
            // בדיקה האם קיים משתמש עם אותו שם משתמש
            var existingBranch = _context.Branches.FirstOrDefault(b => b.Name == b.Name);

            if (existingBranch == null) // אם לא קיים משתמש עם שם משתמש זהה
            {
                _context.Branches.Add(branch);
                _context.SaveChanges();
            }

        }
        public void UpdateBranch(int Id, Branch branch)
        {
            var branchToUpdate = _context.Branches.Find(Id);
            if (branchToUpdate != null)
            {
                if (!branchToUpdate.Name.Equals(branch.Name))
                {
                    branchToUpdate.Name = branch.Name;

                }
                if(branchToUpdate.Email != branch.Email)
                {
                    branchToUpdate.Email = branch.Email;
                }
                if(branchToUpdate.Address != branch.Address)
                {
                    branchToUpdate.Address = branch.Address;
                }
                if(branchToUpdate.Phone != branch.Phone) { 
                    branchToUpdate.Phone = branch.Phone;
                }
                _context.SaveChanges();

            }

        }
        public void DeleteBranch(int Id)
        {
            var branchToRemove = _context.Branches.Find(Id);
            if (branchToRemove != null)
            {
                _context.Branches.Remove(branchToRemove);
                _context.SaveChanges();
            }
        }
    }
}
