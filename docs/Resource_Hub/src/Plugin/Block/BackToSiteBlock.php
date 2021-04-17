<?php

namespace Drupal\resourcehub\Plugin\Block;

use Drupal\Core\Block\BlockBase;
use Drupal\Core\Config\ConfigFactoryInterface;
use Drupal\Core\Plugin\ContainerFactoryPluginInterface;
use Drupal\Core\Url;
use Symfony\Component\DependencyInjection\ContainerInterface;

/**
 * Provides a back to site block.
 *
 * @Block(
 *   id = "resourcehub_back_to_site",
 *   admin_label = @Translation("Back to site"),
 *   category = @Translation("Resource hub")
 * )
 */
class BackToSiteBlock extends BlockBase implements ContainerFactoryPluginInterface {

  /**
   * The config factory.
   *
   * @var \Drupal\Core\Config\ConfigFactoryInterface
   */
  protected $configFactory;

  /**
   * Constructs a new BackToSiteBlock instance.
   *
   * @param array $configuration
   *   The plugin configuration, i.e. an array with configuration values keyed
   *   by configuration option name. The special key 'context' may be used to
   *   initialize the defined contexts by setting it to an array of context
   *   values keyed by context names.
   * @param string $plugin_id
   *   The plugin_id for the plugin instance.
   * @param mixed $plugin_definition
   *   The plugin implementation definition.
   * @param \Drupal\Core\Config\ConfigFactoryInterface $config_factory
   *   The config factory.
   */
  public function __construct(array $configuration, $plugin_id, $plugin_definition, ConfigFactoryInterface $config_factory) {
    parent::__construct($configuration, $plugin_id, $plugin_definition);
    $this->configFactory = $config_factory;
  }

  /**
   * {@inheritdoc}
   */
  public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition) {
    return new static(
      $configuration,
      $plugin_id,
      $plugin_definition,
      $container->get('config.factory')
    );
  }

  /**
   * {@inheritdoc}
   */
  public function build() {
    $settings = $this->configFactory->get('resourcehub.settings');
    $mainSiteUrl = $settings->get('main_site_link.url');
    $cache = [
      'tags' => ['config:resourcehub.settings'],
    ];
    if ($mainSiteUrl) {
      return [
        '#type' => 'link',
        '#url' => Url::fromUri($mainSiteUrl),
        '#title' => $settings->get('main_site_link.text') ?: 'Back to site',
        '#cache' => $cache,
      ];
    }
    else {
      return ['#cache' => $cache];
    }
  }

}
