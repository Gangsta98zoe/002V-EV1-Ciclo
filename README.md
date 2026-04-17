# Plantillas CI/CD - TechMarket v1.0.0

Este repositorio centraliza el diseño, integración y parametrización de plantillas reutilizables orientadas a la estandarización de pipelines de CI/CD para TechMarket. El objetivo es optimizar el ciclo de vida del software mediante una capa de abstracción sólida y modular.

### 🛠️ Estructura del Proyecto
Las plantillas se encuentran organizadas bajo el estándar de GitHub Actions:
- `.github/workflows/templates/template_build.yml`: Gestión de construcción y artefactos.
- `.github/workflows/templates/template_test.yml`: Ejecución de pruebas unitarias y de integración.
- `.github/workflows/templates/template_deploy.yml`: Despliegue automatizado en entornos AWS.

### ⚙️ Documentación de Parámetros (Ítem 1.3)
Para modificar el comportamiento de los flujos sin alterar la plantilla base, utilice los siguientes parámetros:

| Parámetro | Plantilla | Descripción | Requerido |
| :--- | :--- | :--- | :--- |
| `node-version` | Build / Test | Versión de Node.js a utilizar (Ej: '18', '20'). | No (Default: '18') |
| `environment` | Build / Deploy | Define el entorno de ejecución (dev, staging, prod). | **Sí** |
| `region` | Deploy | Región geográfica de destino en AWS. | **Sí** |
| `test-command` | Test | Comando específico para ejecutar las pruebas. | No (Default: 'npm test') |

### 🛡️ Justificación de Acciones Externas (Ítem 1.2)

#### 1. `actions/checkout@v4`
* **Propósito:** Clonar el código fuente en el entorno de ejecución.
* **Eficiencia y Seguridad:** Es la acción oficial más optimizada y segura para el manejo de código, garantizando integridad en cada ejecución.

#### 2. `actions/setup-node@v4`
* **Propósito:** Configurar el entorno Node.js requerido.
* **Compatibilidad:** Su parametrización permite que diferentes equipos utilicen versiones de Node superiores sin romper la consistencia del pipeline.

#### 3. `aws-actions/configure-aws-credentials@v4`
* **Propósito:** Configuración segura de credenciales AWS.
* **Seguridad:** Implementa **OIDC**, eliminando la necesidad de almacenar llaves de acceso permanentes en GitHub, mitigando riesgos de seguridad.

### 📊 Ejemplos de Reutilización por Equipos (Ítem 1.3)

Para que los equipos de TechMarket integren estas plantillas en sus propios flujos, deben utilizar la sintaxis `uses` como se muestra a continuación:

**Ejemplo A: Equipo Backend (Entorno de Staging)**
```yaml
jobs:
  build-backend:
    uses: ./.github/workflows/templates/template_build.yml
    with:
      environment: 'staging'
      node-version: '20'

  test-backend:
    needs: build-backend
    uses: ./.github/workflows/templates/template_test.yml
    with:
      node-version: '20'
      test-command: 'npm run test:integration'


### Guía de Configuración de Parámetros (Abstracción)

Para asegurar la integridad del sistema de CI/CD, **no es necesario modificar los archivos dentro de `.github/workflows/templates/`**. La personalización se realiza exclusivamente desde el workflow que invoca la plantilla mediante la cláusula `with`.

#### Cómo modificar el comportamiento:
1.  **Localice su archivo de Pipeline Principal:** Generalmente `.github/workflows/main_pipeline.yml`.
2.  **Identifique el Job a modificar:** (ej: `build`, `test` o `deploy`).
3.  **Ajuste los valores en la sección `with`:**
    * **Para cambiar versiones:** Modifique el valor de `node-version`.
    * **Para cambiar destinos:** Ajuste `environment` o `region`.
    * **Para comandos de prueba:** Edite `test-command`.

#### Ejemplo de Modificación Segura:
Si el equipo necesita subir la versión de Node de la 18 a la 20, el cambio se realiza así en el pipeline principal:

```yaml
# FORMA CORRECTA: Modificar solo el pipeline principal
  test:
    uses: ./.github/workflows/templates/template_test.yml
    with:
      node-version: '20' # Solo cambie este valor aquí
```

> [!IMPORTANT]
> **Regla de Oro:** Si un parámetro no está definido en la tabla de parámetros superior, solicite una actualización al equipo de infraestructura en lugar de modificar la plantilla base. Esto evita que sus cambios afecten accidentalmente a otros equipos de la organización.


