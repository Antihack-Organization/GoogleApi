﻿using System;
using System.Globalization;
using GoogleApi.Entities.Common;

namespace GoogleApi.Entities.Places.Search.Radar.Request
{
    /// <summary>
    /// PlacesRadarSearch Request.
    /// A Radar Search request must include at least one of keyword, name, or types.
    /// </summary>
    public class PlacesRadarSearchRequest : BasePlacesSearchRequest
    {
        /// <summary>
        /// Base Url.
        /// </summary>
        protected internal override string BaseUrl => base.BaseUrl + "radarsearch/json";

        /// <summary>
        /// name — One or more terms to be matched against the names of places, separated with a space character. 
        /// Results will be restricted to those containing the passed name values. 
        /// Note that a place may have additional names associated with it, beyond its listed name. 
        /// The API will try to match the passed name value against all of these names. As a result, places may be returned in the results 
        /// whose listed names do not match the search term, but whose associated names do.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// keyword — A term to be matched against all content that Google has indexed for this place, 
        /// including but not limited to name, type, and address, as well as customer reviews and other third-party content.
        /// </summary>
        public virtual string Keyword { get; set; }

        /// <summary>
        /// Radius (required).
        /// The distance (in meters) within which to return Place results. 
        /// Note that setting a radius biases results to the indicated area, but may not fully restrict results to the specified area. 
        /// See Location Biasing below.
        /// </summary>
        public virtual double? Radius { get; set; }

        /// <summary>
        /// Location (required).
        /// The point around which you wish to retrieve Place information. 
        /// Must be specified as latitude,longitude.
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// See <see cref="BasePlacesSearchRequest.GetQueryStringParameters()"/>.
        /// </summary>
        /// <returns>The <see cref="QueryStringParameters"/> collection.</returns>
        public override QueryStringParameters GetQueryStringParameters()
        {
            if (this.Location == null)
                throw new ArgumentException("Location is required");

            if (this.Radius == null)
                throw new ArgumentException("Radius is required");

            if (this.Radius.HasValue && (this.Radius > 50000 || this.Radius < 1))
                throw new ArgumentException("Radius must be greater than or equal to 1 and less than or equal to 50.000");

            if (string.IsNullOrWhiteSpace(this.Keyword) && string.IsNullOrWhiteSpace(this.Name) && !this.Type.HasValue)
                throw new ArgumentException("Keyword, Name or Type is required");

            var parameters = base.GetQueryStringParameters();

            parameters.Add("location", this.Location.ToString());
            parameters.Add("radius", this.Radius.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrWhiteSpace(this.Name))
                parameters.Add("name", this.Name);

            if (!string.IsNullOrWhiteSpace(this.Keyword))
                parameters.Add("keyword", this.Keyword);

            return parameters;
        }
    }
}