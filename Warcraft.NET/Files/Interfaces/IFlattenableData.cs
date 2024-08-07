﻿//
//  IFlattenableData.cs
//
//  Copyright (c) 2018 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.Collections.Generic;

namespace Warcraft.NET.Files.Interfaces
{
    /// <summary>
    /// Defines an interface which provides a way to flatten a data type into a collection of another data type.
    /// </summary>
    /// <typeparam name="T">The data type to flatten to.</typeparam>
    public interface IFlattenableData<out T>
    {
        /// <summary>
        /// Flattens the object into a collection of <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A read-only collection.</returns>
        IReadOnlyCollection<T> Flatten();
    }
}