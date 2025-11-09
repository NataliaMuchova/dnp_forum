using System;

namespace DTOs;

public class UpdateCommentDto
{
    public required string Body { get; set; }

    public UpdateCommentDto(string body)
    {
        Body = body;
    }
}
