using System;
using System.Collections.Generic;

namespace Condorcet.B2.AspnetCore.MVC.Application.Models
{
    public class DashboardViewModel
    {
        /// <summary>
        /// Nombre total d'utilisateurs inscrits
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// Nombre total de produits disponibles
        /// </summary>
        public int ProductCount { get; set; }

        /// <summary>
        /// Nombre total de commandes simulées
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// Liste des derniers utilisateurs inscrits
        /// </summary>
        public IEnumerable<RecentUserDto> RecentUsers { get; set; }

        /// <summary>
        /// Liste des derniers produits ajoutés
        /// </summary>
        public IEnumerable<RecentProductDto> RecentProducts { get; set; }
    }

    public class RecentUserDto
    {

        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecentProductDto
    {
        /// <summary>
        /// Nom du produit
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date d'ajout du produit
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}