namespace backend.Data.Entities
{
	public class FollowMapping
	{
		public int FollowerId { get; init; }
		public User? Follower { get; init; }

		public int FolloweeId { get; init; }
		public User? Followee { get; init; }
	}
}
