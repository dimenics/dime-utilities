﻿using System.ComponentModel;

namespace Dime.Utilities.Tests
{
    public enum MockupEnum
    {
        [Description("Item0Description")]
        Item0 = 0,

        [Description("Item1Description")]
        Item1 = 1,

        [Description("Item2Description")]
        Item2 = 2,

        [Description("")]
        Item3 = 3
    }

    public enum Crud
    {
        Create,
        Update,
        Delete
    }
}