/**
 * @file
 * Resource Hub Default behaviors.
 */

(function ($, Drupal) {

  'use strict';

  /**
   * Behavior description.
   */
  Drupal.behaviors.resourcehubTheme = {
    attach: function (context, settings) {

      console.log('It works!');

    }
  };
  Drupal.behaviors.accordiontoggle = {
    attach: function attach(context, settings) {
      if (context !== document) {
        return;
      }
      // Create the button
      var button = document.createElement("button");
      button.innerHTML = "Show all filters";
      button.setAttribute("aria-expanded", "false");
      button.classList.add('facet-control');
      button.setAttribute('aria-control', "#facet-wrapper");
      // Append after the summary block
      var summary = document.getElementsByClassName("block-facets-summary-blockresources-summary")[0];


      // Create a wrapper around the facets
      var sidebar = document.querySelector('.region-sidebar-first');
      // now let's grab the children of that node and strip the first two:
      var nodes = [...sidebar.children].splice(2);
      // now let's create the wrapper div
      var wrapper = document.createElement('div');
      wrapper.classList.add('facet-wrapper');
      wrapper.id ="#facet-wrapper";
      // and append all children:
      nodes.map(node => wrapper.appendChild(node));
      // and add the wrapper to the container:
      sidebar.appendChild(wrapper);

      // Set the attributes on the section, depending on the media query
      var facets = document.getElementsByClassName("facet-wrapper");
      var i;
      function setDefaultsbyScreensize(x) {
        if (x.matches) { // If media query matches
          for (i = 0; i < facets.length; i++) {
            facets[i].style.display = "none";
            facets[i].setAttribute("aria-hidden", "true");
          }
          // Add the button only for smaller screens.
          summary.append(button);
          button.innerHTML = "Show all filters";
          button.setAttribute("aria-expanded", "false");

        } else {
          for (i = 0; i < facets.length; i++) {
            facets[i].style.display = "block";
            facets[i].setAttribute("aria-hidden", "false");
          }
          button.innerHTML = "Hide all filters";
          button.setAttribute("aria-expanded", "true");
        }
      }

      var x = window.matchMedia("(max-width: 767px)")
      setDefaultsbyScreensize(x) // Call listener function at run time
      x.addListener(setDefaultsbyScreensize) // Attach listener function on state changes


      // Add event handler
      button.addEventListener ("click", function() {
        // Toggle the attributes depending on the state of the button
        if (button.attributes['aria-expanded'].value == "false") {
          button.innerHTML = "Hide all filters";
          button.setAttribute("aria-expanded", "true");
          for (i = 0; i < facets.length; i++) {
            facets[i].style.display = "block";
            facets[i].setAttribute("aria-hidden", "false");
          }
        } else {
          button.setAttribute("aria-expanded", "false");
          button.innerHTML = "Show all filters";
          for (i = 0; i < facets.length; i++) {
            facets[i].style.display = "none";
            facets[i].setAttribute("aria-hidden", "true");
          }
        }
      });
    }
  };
}(jQuery, Drupal));
