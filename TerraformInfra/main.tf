# Aislamiento de red para los servicios de monitoreo
resource "docker_network" "monitoring_network" {
  name = "academic_monitoring_net"
}

# Declaración de recursos de imagen
resource "docker_image" "prometheus" {
  name         = "prom/prometheus:latest"
  keep_locally = false
}

resource "docker_image" "grafana" {
  name         = "grafana/grafana:latest"
  keep_locally = false
}

# Despliegue del motor de métricas
resource "docker_container" "prometheus_server" {
  image = docker_image.prometheus.image_id
  name  = "academico_prometheus"
  
  ports {
    internal = 9090
    external = 9090
  }

  networks_advanced {
    name = docker_network.monitoring_network.name
  }

  volumes {
    host_path      = "${abspath(path.cwd)}/prometheus.yml"
    container_path = "/etc/prometheus/prometheus.yml"
  }
}

# Despliegue del panel de visualización
resource "docker_container" "grafana_dashboard" {
  image = docker_image.grafana.image_id
  name  = "academico_grafana"
  
  ports {
    internal = 3000
    external = 3000
  }

  networks_advanced {
    name = docker_network.monitoring_network.name
  }
}