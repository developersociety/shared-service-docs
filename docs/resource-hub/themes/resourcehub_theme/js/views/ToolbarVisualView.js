/**
 * The responsive_menu module was overriding the toolbar module's
 * JS to apply padding to the responsive menu wrapper rather than
 * body however when using the Claro toolbar module this isn't
 * necessary as the toolbar is position:fixed.
 *
 * This is just a temporary workaround while we develop a fix
 * in responsive_menu.
 *
 * @see responsive_menu/js/views/ToolbarVisualView.js
 */

(function ($, Drupal, drupalSettings, Backbone) {
  "use strict";
  Drupal.toolbar.ToolbarVisualView.prototype.updateToolbarHeight = function() {}
})(jQuery, Drupal, drupalSettings, Backbone);
