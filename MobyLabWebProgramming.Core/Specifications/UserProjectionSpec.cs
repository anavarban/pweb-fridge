using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserProjectionSpec : BaseSpec<UserProjectionSpec, User, UserDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<User, UserDTO>> Spec => e => new()
    {
        Id = e.Id,
        Email = e.Email,
        Name = e.Name,
        Role = e.Role,
        Fridge = e.Fridge != null ? new FridgeDTO
        {
            Id = e.Fridge.Id,
            Name = e.Fridge.Name,
            UserId = e.Fridge.UserId,
            CreatedAt = e.Fridge.CreatedAt,
            UpdatedAt = e.Fridge.UpdatedAt,
            Ingredients = e.Fridge.Ingredients,
        } : null
    };

    public UserProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
        Query.Include(e => e.Fridge);
    }

    public UserProjectionSpec(Guid id) : base(id)
    {
        Query.Include(e => e.Fridge); // This is an example on how to include related entities in the query.
    }

    public UserProjectionSpec(string? search)
    {

        Query.Include(e => e.Fridge);
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                                           // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
