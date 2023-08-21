// Copyright (c) 2023 James Frost

// Licensed under the MIT License.
// See the LICENSE.txt file in the project root for the full license text.

using System;

namespace NoteBuilder.Model
{
    public class NoteBlock
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid Id { get; set; }
        public bool Placeholder { get; set; }

        public NoteBlock(string title, string content, Guid id, bool placeholder)
        {
            Title = title;
            Content = content;
            Id = id;
            Placeholder = placeholder;
        }
    }
}
