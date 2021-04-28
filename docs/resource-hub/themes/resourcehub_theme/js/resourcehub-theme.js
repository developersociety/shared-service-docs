/**
 * @file
 * Resource Hub Default behaviors.
 */

(function ( Drupal) {

  'use strict';

  /**
   * Behaviour to provide a toggle to show\hide Facet blocks.
   */
  Drupal.behaviors.facetBlockToggle = {
    attach: function attach(context, settings) {
      if (context !== document) {
        return;
      }

      var title = document.createElement("h2");
      title.innerHTML = "Refine your search";
      var summary = document.getElementsByClassName("block-facets-summary-blockresources-summary")[0];
      summary.prepend(title);

      // Create the toggle all button
      var button = document.createElement("button");
      button.innerHTML = "Show all filters";
      button.setAttribute("aria-expanded", "false");
      button.classList.add('facet-control');
      button.setAttribute('aria-control', "#facet-wrapper");

      var summary = document.getElementsByClassName("block-facets-summary-blockresources-summary")[0];
      summary.append(button);


      var sideBar = document.getElementsByClassName("region-sidebar-first")[0];

      // Wrap the facet blocks in a div so we can easily toggle
      // the visibility of all facets.
      var facetBlocks = sideBar.getElementsByClassName("block-facets");
      var facetsWrapper = document.createElement("div");
      facetsWrapper.classList.add("facet-wrapper");
      facetsWrapper.id = "#facet-wrapper";
      while (facetBlocks.length > 0) {
        facetsWrapper.appendChild(facetBlocks[0]);
      }
      sideBar.appendChild(facetsWrapper);

      function toggleFilterDisplay(open) {
        facetsWrapper.style.display = open ? "block" : "none";
        facetsWrapper.setAttribute("aria-hidden", open ? "false" : "true");
      }

      function toggleButtonState(open) {
        button.innerHTML = open ? "Hide all filters" : "Show all filters";
        button.setAttribute("aria-expanded", open ? "true" : "false");
      }

      function toggleButtonDisplay(visible) {
        button.style.display = visible ? "block" : "none";
      }

      function setDefaultsbyScreensize(x) {
        var isMobile = x.matches;
        toggleFilterDisplay(!isMobile);
        toggleButtonState(!isMobile);
        toggleButtonDisplay(isMobile)
      }

      var x = window.matchMedia("(max-width: 767px)")
      setDefaultsbyScreensize(x) // Call listener function at run time
      x.addListener(setDefaultsbyScreensize) // Attach listener function on state changes

      // Add event handler
      button.addEventListener ("click", function() {
        // Toggle the attributes depending on the state of the button
        var isOpen = button.attributes['aria-expanded'].value === "true";
        toggleFilterDisplay(!isOpen);
        toggleButtonState(!isOpen);
      });

    }
  };
}(Drupal));
