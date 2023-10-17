using WebApi.Models;

namespace WebApi.Data;

public interface IJobRepository
{
	Task<Job> AddAsync(Job job);

	Task<Job> UpdateAsync(Job job);

	IEnumerable<Job> GetAll();
}
