using WebApi.Models;

namespace WebApi.Data;

public class JobRepository : IJobRepository
{
	private readonly AppDbContext _context;

	public JobRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Job> AddAsync(Job job)
	{
		_context.Jobs.Add(job);
		await _context.SaveChangesAsync();
		return job;
	}

	public async Task<Job> UpdateAsync(Job job)
	{
		_context.Jobs.Update(job);
		await _context.SaveChangesAsync();
		return job;
	}

	public IEnumerable<Job> GetAll()
	{
		return _context.Jobs.OrderByDescending(j => j.StartTime).ToList();
	}
}
