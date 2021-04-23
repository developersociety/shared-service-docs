<?php

namespace Drupal\resourcehub_styleguide\Plugin\Styleguide;

use Drupal\Core\Entity\EntityTypeManager;
use Drupal\styleguide\GeneratorInterface;
use Drupal\styleguide\Plugin\StyleguidePluginBase;
use Symfony\Component\DependencyInjection\ContainerInterface;

/**
 * Default Styleguide items implementation.
 *
 * @Plugin(
 *   id = "resourcehub_styleguide",
 *   label = @Translation("Default Styleguide elements")
 * )
 */
class ResourceHubStyleguide extends StyleguidePluginBase {

  /**
   * The styleguide generator service.
   *
   * @var \Drupal\styleguide\Generator
   */
  protected $generator;

  /**
   * The entity type manager service.
   *
   * @var \Drupal\Core\Entity\EntityTypeManager
   */
  protected $entityTypeManager;

  /**
   * Constructs a new defaultStyleguide.
   *
   * @param array $configuration
   *   A configuration array containing information about the plugin instance.
   * @param string $plugin_id
   *   The plugin_id for the plugin instance.
   * @param array $plugin_definition
   *   The plugin implementation definition.
   * @param \Drupal\styleguide\GeneratorInterface $styleguide_generator
   *   The styleguide generator service.
   * @param \Drupal\Core\Entity\EntityTypeManager $entity_type_manager
   *   The entity type manager service.
   *
   * @internal param \Drupal\styleguide\GeneratorInterface $generator
   */
  public function __construct(array $configuration, $plugin_id, array $plugin_definition, GeneratorInterface $styleguide_generator, EntityTypeManager $entity_type_manager) {
    parent::__construct($configuration, $plugin_id, $plugin_definition);

    $this->generator = $styleguide_generator;
    $this->entityTypeManager = $entity_type_manager;
  }

  /**
   * {@inheritdoc}
   */
  public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition) {
    return new static(
      $configuration,
      $plugin_id,
      $plugin_definition,
      $container->get('styleguide.generator'),
      $container->get('entity_type.manager'),
    );
  }

  /**
   * {@inheritdoc}
   */
  public function items() {
    $paragraphs = [
      'Video' => '62beed54-8646-466b-b2ce-724e9b3c83d0',
      'Text' => '40398934-0cd4-4cac-bdce-abfe42ea06ce',
      'Image' => 'e8af6601-746b-4a47-9a75-90d16adf7021',
      'Document' => 'f0d35e81-7e66-4aef-a38b-2944685c5b99',
      'Audio' => '6da67205-5472-44d4-8bae-52fe7ba4917e',
      'External link' => 'b031d68a-f052-4eaf-87e3-469bfa17269c',
      'Link block' => 's5d2c80e-3af0-4dc2-a05c-209e7601680f',
    ];
    $viewBuilder = $this->entityTypeManager->getViewBuilder('paragraph');
    $storage = $this->entityTypeManager->getStorage('paragraph');
    foreach ($paragraphs as $paragraphType => $paragraphUuid) {
      $paragraph = $storage->loadByProperties(['uuid' => $paragraphUuid]);
      if (!empty($paragraph)) {
        $items[strtolower("resourcehub_$paragraphType")] = [
          'title' => $this->t('Resource Hub @label', ['@label' => $paragraphType]),
          'content' => $viewBuilder->view(reset($paragraph)),
          'group' => $this->t('Resource Hub'),
        ];
      }
    }
    return $items;
  }

}
