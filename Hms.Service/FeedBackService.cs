using HMS.Data.Repository;
using Hms.Models.ViewModels;
using Hms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hms.Service
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IRepository _repository;

        public FeedBackService(IRepository repository)
        {
            _repository = repository;
        }

        //Todo : Ctrl + k + D in all files for proper spacing and indentation

        public async Task<FeedBack> AddFeedBackAsync(FeedBackViewModel feedBackViewModel)
        {
            var feedBack = new FeedBack()
            {
                Description = feedBackViewModel.Description,
                UserID = feedBackViewModel.UserId,
            };
            await _repository.Create(feedBack);
            await _repository.SaveAsync();

            return feedBack;
        }

        public Task<List<FeedBack>> GetAllFeedBacks()
        {
            var feedbacks = _repository.FindAll<FeedBack>().ToListAsync();
            return feedbacks;
        }

        public async Task<FeedBack> GetFeedBackbyID(int id)
        {
            var feedBack = await _repository.GetByID<FeedBack>(id);
            return feedBack;
        }

        public async Task<List<FeedBack>> GetFeedBacksByUserId(string userId)
        {
            var feedBacklst = await _repository.FindByCondition<FeedBack>(l => l.UserID == userId).ToListAsync();
            return feedBacklst;
        }

        public async Task<bool> UpdateFeedBackAsync(int id, FeedBackViewModel feedBackViewModel)
        {
            var feedBack = new FeedBack()
            {
                FeedbackId = id,
                Description = feedBackViewModel.Description,
                UserID = feedBackViewModel.UserId,
            };
            _repository.Update(feedBack);
            await _repository.SaveAsync();
            return true;
        }

        public bool Delete(FeedBack feedBack)
        {
            if (feedBack != null)
            {
                _repository.Delete(feedBack);
                _repository.Save();
                return true;
            }

            return false;
        }
    }

    public interface IFeedBackService
    {
        Task<FeedBack> AddFeedBackAsync(FeedBackViewModel feedBackViewModel);
        Task<List<FeedBack>> GetAllFeedBacks();
        Task<FeedBack> GetFeedBackbyID(int id);
        Task<List<FeedBack>> GetFeedBacksByUserId(string userId);
        Task<bool> UpdateFeedBackAsync(int id, FeedBackViewModel feedBackViewModel);
        bool Delete(FeedBack feedBack);

    }
}
