using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BarbudosShop.Components.Account
{
    public sealed class IdentityRedirectManager
    {
        private readonly NavigationManager navigationManager;

        public const string StatusCookieName = "Identity.StatusMessage";

        private static readonly CookieBuilder StatusCookieBuilder = new()
        {
            SameSite = SameSiteMode.Strict,
            HttpOnly = true,
            IsEssential = true,
            MaxAge = TimeSpan.FromSeconds(5),
        };

        public IdentityRedirectManager(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        public void RedirectTo(string? uri)
        {
            uri ??= "";

            if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
            {
                uri = navigationManager.ToBaseRelativePath(uri);
            }

            navigationManager.NavigateTo(uri);
        }

        public void RedirectTo(string uri, Dictionary<string, object?> queryParameters)
        {
            var uriWithoutQuery = navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
            var newUri = navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
            RedirectTo(newUri);
        }

        public void RedirectToWithStatus(string uri, string message, HttpContext context)
        {
            context.Response.Cookies.Append(StatusCookieName, message, StatusCookieBuilder.Build(context));
            RedirectTo(uri);
        }

        public void RedirectToCurrentPageWithStatus(string message, HttpContext context)
        {
            var currentUri = navigationManager.Uri;
            context.Response.Cookies.Append(StatusCookieName, message, StatusCookieBuilder.Build(context));
            navigationManager.NavigateTo(currentUri);
        }

        public void RedirectToCurrentPage()
        {
            var currentUri = navigationManager.Uri;
            navigationManager.NavigateTo(currentUri);
        }
    }
}
