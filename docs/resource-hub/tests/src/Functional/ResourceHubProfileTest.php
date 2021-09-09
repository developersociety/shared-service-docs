<?php

namespace Drupal\Tests\resourceprofile\Functional;

use Drupal\Tests\BrowserTestBase;
use Symfony\Component\HttpFoundation\Response;

/**
 * Functional tests for LocalGovDrupal install profile.
 */
class ResourceHubProfileTest extends BrowserTestBase {

  /**
   * {@inheritdoc}
   */
  protected $profile = 'resourcehub';

  /**
   * {@inheritdoc}
   */
  protected $defaultTheme = 'resourcehub_theme';

  /**
   * The content editor user to test with.
   *
   * @var \Drupal\user\Entity\User
   */
  protected $editorUser;

  /**
   * The site administrator user to test with.
   *
   * @var \Drupal\user\Entity\User
   */
  protected $siteAdminUser;

  /**
   * {@inheritdoc}
   */
  protected function setUp(): void {
    parent::setUp();
    $this->editorUser = $this->createUser([], NULL, FALSE,
      ['name' => 'editor', 'roles' => ['content_editor']]
    );
    $this->siteAdminUser = $this->createUser([], NULL, FALSE, ['roles' => ['site_administrator']]);
  }

  /**
   * {@inheritdoc}
   */
  protected function installParameters() {
    $parameters = parent::installParameters();
    if ($this->getName(FALSE) === 'testResourceHubDemoContent') {
      $parameters['forms']['install_configure_form']['install_demo'] = 1;
    }
    return $parameters;
  }

  /**
   * Test the set up of the demo environment.
   *
   * @see self::installParameters
   */
  public function testResourceHubDemoContent() {
    $this->assertTrue(\Drupal::moduleHandler()->moduleExists('resourcehub_demo'));
    // @todo ? assert existence of content via default_content
  }

  /**
   * Test the set up of the default environment.
   */
  public function testResourceHubDefaultContent() {
    $this->assertFalse(\Drupal::moduleHandler()->moduleExists('resourcehub_demo'));
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_audience/overview');
    $this->assertSession()->pageTextContains('Content designers');
    $this->assertSession()->pageTextContains('UX');
    $this->assertSession()->pageTextContains('Visual designers');
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_resource_type/overview');
    $this->assertSession()->pageTextContains('Guidance');
    $this->assertSession()->pageTextContains('Advice');
    $this->assertSession()->pageTextContains('Templates');
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_format/overview');
    $this->assertSession()->pageTextContains('Audio');
    $this->assertSession()->pageTextContains('Video');
    $this->assertSession()->pageTextContains('Blended content');
  }

  /**
   * Test site manager permissions .
   */
  public function testResourceHubSiteManager() {
    $siteManagerUser = $this->createUser([
      'administer nodes',
      'access content overview',
    ]);

    $this->drupalLogin($siteManagerUser);
    $this->assertSession()->pageTextContains('Member for');
    $this->drupalGet('/admin/content');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
  }

  /**
   * Test resource hub settings.
   */
  public function testResourceHubSettings() {

    $this->drupalGet('/admin/resourcehub-settings');
    $this->assertSession()->statusCodeEquals(Response::HTTP_FORBIDDEN);
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('/admin/resourcehub-settings');
    $this->assertSession()->statusCodeEquals(Response::HTTP_FORBIDDEN);
    $this->drupalLogout();
    $this->drupalLogin($this->siteAdminUser);
    $this->assertSession()->linkByHrefExists('/admin/resourcehub-settings');
    $this->drupalGet('/admin/resourcehub-settings/blocks');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->fieldExists('Legal copy');
    $this->assertSession()->fieldExists('Main menu display');
    $page = $this->getSession()->getPage();
    $page->fillField('Legal copy', 'Testing legal copy');
    $page->fillField('main_site_link[url]', 'http://www.backtosite.com');
    $page->fillField('main_site_link[text]', 'Back to site text');
    $this->click('#edit-submit');
    $this->assertTrue(\Drupal::config('resourcehub.settings')->get('legal') == 'Testing legal copy');
    $this->assertTrue(\Drupal::config('resourcehub.settings')->get('main_site_link.url') == 'http://www.backtosite.com');
    $this->assertTrue(\Drupal::config('resourcehub.settings')->get('main_site_link.text') == 'Back to site text');
    $this->assertSession()->elementContains('css', '#edit-legal', 'Testing legal copy');
    $this->assertSession()->elementAttributeContains('css', '[name="main_site_link[url]"]', 'value', 'http://www.backtosite.com');
    $this->assertSession()->elementAttributeContains('css', '[name="main_site_link[text]"]', 'value', 'Back to site text');
    $this->drupalGet('admin/resourcehub-settings/theme');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->fieldExists('Color set');
    $this->assertSession()->fieldExists('Accent');
  }

  /**
   * Test that the main menu display can be configured.
   */
  public function testMainMenu() {
    $this->drupalGet('<front>');
    $page = $this->getSession()->getPage();
    $this->assertEmpty($page->findById('toggle-icon'));
    $this->drupalLogin($this->siteAdminUser);
    $this->drupalGet('/admin/structure/menu/manage/main/add');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $page = $this->getSession()->getPage();
    $page->fillField('title[0][value]', 'Home');
    $page->fillField('link[0][uri]', '<front>');
    $this->click('#edit-submit');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->drupalGet('<front>');
    $page = $this->getSession()->getPage();
    $this->assertEmpty($page->findById('toggle-icon'));
    $this->drupalGet('/admin/resourcehub-settings/blocks');
    $page->checkField('main_menu_display');
    $this->click('#edit-submit');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->drupalGet('<front>');
    $page = $this->getSession()->getPage();
    $this->assertNotEmpty($page->findById('toggle-icon'));
    $menuLink = $page->find('css', '#off-canvas > ul > li > a[href="/"]');
    $this->assertNotEmpty($menuLink);
    $this->assertEquals('Home', $menuLink->getText());
  }

  /**
   * Test site manager can manage taxonomy.
   */
  public function testManageTaxonomy() {
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('/admin/structure/taxonomy');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Audience');
    $this->assertSession()->pageTextContains('Resource');
    $this->assertSession()->pageTextContains('Topics');
    // Confirm add term page is available to the site manager.
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_audience/add');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Add term');
    // Fill out the name of the term.
    $page = $this->getSession()->getPage();
    $page->fillField('name[0][value]', 'Resourcey Audience');
    // Submit the form.
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Created new term Resourcey Audience');
    // Return to the overview page.
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_audience/overview');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Audience');
    $this->assertSession()->pageTextContains('Resourcey Audience');
    // Edit the term.
    $this->drupalGet('/taxonomy/term/1/edit');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $page = $this->getSession()->getPage();
    $page->fillField('name[0][value]', 'Resource Audience!');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Updated term Resource Audience!');
    // Delete the term.
    $this->drupalGet('/taxonomy/term/1/edit');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $page = $this->getSession()->getPage();
    $this->click('[data-drupal-selector="edit-delete"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Deleted term Resource Audience!');
    // Check the Resource Type taxonomy.
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_resource_type/add');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Add term');
    // Fill out the name of the term.
    $page = $this->getSession()->getPage();
    $page->fillField('name[0][value]', 'Resourcey Resource Type');
    // Submit the form.
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Created new term Resourcey Resource Type');
    // Check the Topic taxonomy.
    $this->drupalGet('/admin/structure/taxonomy/manage/resourcehub_topics/add');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Add term');
    // Fill out the name of the term.
    $page = $this->getSession()->getPage();
    $page->fillField('name[0][value]', 'Resourcey Topic');
    // Submit the form.
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Created new term Resourcey Topic');
  }

  /**
   * Test Resource content type create, edit, delete.
   */
  public function testResourceContentTypeCrud() {

    // Test the editor can access the add form.
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('node/add/resource');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);

    $page = $this->getSession()->getPage();
    $page->fillField('title[0][value]', 'Resourcey title');
    $page->fillField('resourcehub_summary[0][value]', 'Resourcey summary');
    $page->fillField('resourcehub_audience_intro[0][value]', 'Resourcey Audience intro');
    // Add a text paragraph.
    $this->click('#resourcehub-primary-resource-resourcehub-text-add-more');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $page->fillField('resourcehub_primary_resource[0][subform][resourcehub_text][0][value]', 'Resourcey body');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Resourcey title');
    $this->assertSession()->pageTextContains('Resourcey summary');
    $this->assertSession()->pageTextContains('Resourcey Audience intro');
    // Edit the resource.
    $this->click('[data-drupal-link-system-path="node/1/edit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Edit Resource Resourcey title');
    $page->fillField('title[0][value]', 'Resourcey title new name');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Resourcey title new name');
    // Edit the resource again.
    $this->clicklink('Edit');
    $this->assertSession()->pageTextContains('Edit Resource Resourcey title new name');
    // Delete the resource.
    $this->click('[data-drupal-selector="edit-delete"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('The Resource Resourcey title new name has been deleted.');
  }

  /**
   * Test Resource search page is accessible.
   */
  public function testResourceSearchPage() {
    // Test front page loads after site install.
    $this->drupalGet('resources');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Search resources');
    $page = $this->getSession()->getPage();
    $page->fillField('s', 'unreasonablesearchterm');
    $this->click('[value="Search"]');
    $this->assertSession()->pageTextContains('no results');
  }

  /**
   * Test Landing page content type create, edit, delete.
   */
  public function testLandingContentTypeCrud() {
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('node/add/resourcehub_landing_page');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->fieldExists('Title');
    $this->assertSession()->fieldExists('Introduction');
    $this->assertSession()->responseContains('Add media');
    $this->assertSession()->pageTextContains('Link block');
    $page = $this->getSession()->getPage();
    $page->fillField('title[0][value]', 'Landing pagey title');
    $page->fillField('resourcehub_summary[0][value]', 'Landing pagey summary');
    $page->fillField('resourcehub_link_block[0][subform][resourcehub_title][0][value]', 'Landing pagey link block title');
    // Save.
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Landing pagey title');
    $this->assertSession()->pageTextContains('Landing pagey summary');
    // Edit the landing page.
    $this->click('[data-drupal-link-system-path="node/1/edit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Edit Landing page Landing pagey title');
    $page->fillField('title[0][value]', 'Landing pagey title new name');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Landing pagey title new name');
    // Edit the landing page again.
    $this->clicklink('Edit');
    $this->assertSession()->pageTextContains('Edit Landing page Landing pagey title new name');
    // Delete the landing page.
    $this->click('[data-drupal-selector="edit-delete"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('The Landing page Landing pagey title new name has been deleted.');
  }

  /**
   * Test Page content type create, edit, delete.
   */
  public function testPageContentTypeCrud() {
    $this->drupalLogin($this->editorUser);
    $this->drupalGet('node/add/resourcehub_page');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Create Page');
    $this->assertSession()->fieldExists('Title');
    $this->assertSession()->fieldExists('Summary');
    $page = $this->getSession()->getPage();
    $page->fillField('title[0][value]', 'Pagey title');
    $page->fillField('resourcehub_summary[0][value]', 'Pagey summary');
    // Add a text paragraph.
    $page->fillField('resourcehub_content[0][subform][resourcehub_text][0][value]', 'Pagey body');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Pagey title');
    $this->assertSession()->pageTextContains('Pagey summary');
    $this->assertSession()->pageTextContains('Pagey body');
    // Edit the page.
    $this->clicklink('Edit');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Edit Page Pagey title');
    $page->fillField('title[0][value]', 'Pagey title new name');
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Pagey title new name');
    // Edit the page again.
    $this->clicklink('Edit');
    // Delete the page.
    $this->click('[data-drupal-selector="edit-delete"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->click('[data-drupal-selector="edit-submit"]');
    $this->assertSession()->statusCodeEquals(Response::HTTP_OK);
    $this->assertSession()->pageTextContains('Pagey title new name has been deleted.');
  }

}
