using backend.Models;

namespace backend.Logic.Articles
{
	/*
	 * A response envelope or wrapper is used REST API responses to add additional metadata to the actual payload.
	 * 
	 * The envelope structure helps standardize the format of responses and provides a consistent way to convey information about the response status, errors, and other contextual details.
	 * 
	 * While records can be mutable, they're primarily intended for supporting immutable data models.
	 */
	public record ArticleWrapper(Article Article);
}
