using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title은 최소 5 문자 이상이어야 합니다.")]
    [MaxLength(280, ErrorMessage = "Title은 최대 280 문자 이하여야 합니다.")]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
