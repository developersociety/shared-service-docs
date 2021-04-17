<?php

$config = json_decode(file_get_contents('config.json'), true);

$repositories = $config['repositories'];

exec('rm -rf docs');
exec('mkdir docs');

$sidebar = '';

foreach ($repositories as $repository) {
    exec('git clone '.$repository['gitUrl'].' temp');
    exec('rm -rf temp/.git');
    exec('mv ./temp/'.trim($repository['sourceDocsDirectory'], '/').' ./docs/'.$repository['outputDirectory']);
    exec('rm -rf temp');

    // Repo title
    $sidebar .= '- '.$repository['outputDirectory'].PHP_EOL;

    $recursiveDirectoryIterator = new RecursiveDirectoryIterator('./docs/'.$repository['outputDirectory']);
    $recursiveIteratorIterator = new RecursiveIteratorIterator($recursiveDirectoryIterator);
    $regexIterator = new RegexIterator($recursiveIteratorIterator, '/.*\.md/i', RecursiveRegexIterator::GET_MATCH);

    // Repo items
    foreach ($regexIterator as [$fileName]) {
        if ($fileName !== mb_strtolower($fileName)) {
            exec('mv "'.$fileName.'" "'.mb_strtolower($fileName).'"');
            $fileName = mb_strtolower($fileName);
        }
        $sidebar .= '  - ['.substr($fileName, mb_strlen('./docs/'.$repository['outputDirectory'].'/'), -3).']('.substr($fileName, mb_strlen('./docs/'), -mb_strlen('.md')).')'.PHP_EOL;
    }
}

$index = <<<INDEX
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Catalyst Shared Service Layer documentation</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="description" content="Helping charities build digital skills and improve their services.">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/docsify-themeable@0/dist/css/theme-simple.css" title="Simple">
    <link href="https://assets-global.website-files.com/6048aaba41858594b67e1803/6048aaba4185854c837e184e_cat-fav.png" rel="shortcut icon" type="image/x-icon"/>
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="//fonts.googleapis.com/css2?family=Poppins:ital,wght@0,400;0,600;1,400;1,600&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/docsify-themeable@0/dist/css/theme-simple.css">
    <!-- Custom theme styles -->
    <style>
      :root {
        --base-font-family: "Poppins", -apple-system, BlinkMacSystemFont, "Segoe UI", Helvetica, Arial, sans-serif;
        --base-color: #58536e;
      }
    </style>
</head>
<body>
  <div id="app"></div>
  <script>
    window.\$docsify = {
      name: 'Docs',
      repo: '',
      logo: '/catalyst.svg',
      loadSidebar: true,
      search: {
        depth: 3,
        noData: 'Not found',
        placeholder: 'Search...'
      },
    }
  </script>
  <!-- Docsify v4 -->
  <script src="//cdn.jsdelivr.net/npm/docsify@4"></script>
  <script src="//cdn.jsdelivr.net/npm/docsify-copy-code@2"></script>
  <script src="//cdn.jsdelivr.net/npm/docsify@4/lib/plugins/search.js"></script>
</body>
</html>
INDEX;

file_put_contents('docs/.nojekyll', '');
file_put_contents('docs/_sidebar.md', $sidebar);
file_put_contents('docs/README.md', '# Shared service elements');
file_put_contents('docs/index.html', $index);
exec('cp catalyst.svg docs/catalyst.svg');
