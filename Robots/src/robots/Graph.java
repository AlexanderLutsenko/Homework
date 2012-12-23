package robots;

import java.util.LinkedList;

public class Graph {

    public Graph(int nodeCount, boolean[][] adjMatrix, int[] robots) {
        nodes = new Node[nodeCount];
        for (int i = 0; i < nodeCount; i++) {
            nodes[i] = new Node();
        }
        for (int i = 0; i < nodeCount; i++) {
            nodes[i].init(adjMatrix, robots[i], i, nodes);
        }
        System.out.println("Граф сформирован");
    }

    public void draw() {
        nodes[0].draw();
    }

    public boolean isSolvable() {

        for (Node node : nodes) {
            if (node.getRobotCount() == 1) {
                if (node.isPainted()) {
                    paintedRobots++;
                } else {
                    unpaintedRobots++;
                }
            }
        }

        if (paintedRobots != 1 && unpaintedRobots != 1) {
            return true;
        } else {
            return false;
        }
    }

    public Node[] getNodes() {
        return nodes;
    }
    private Node[] nodes;
    private int paintedRobots = 0;
    private int unpaintedRobots = 0;
}

class Node {

    public void init(boolean[][] matrix, int robotCount, int number, Node[] nodes) {
        isActive = true;
        this.robotCount = robotCount;
        connections = new LinkedList<>();
        for (int j = 0; j < matrix.length; j++) {
            if (matrix[number][j] == true) {
                connections.add(nodes[j]);
            }
        }
    }

    public void mark() {
        for (Node node : connections) {
            node.draw();
        }
    }

    public void draw() {
        if (isActive) {
            isActive = false;
            for (Node node : connections) {
                node.setPainted();
                node.mark();
            }
        }
    }

    public boolean isPainted() {
        return isPainted;
    }

    public int getRobotCount() {
        return robotCount;
    }

    public boolean isConnectedWith(Node node) {
        return connections.contains(node);
    }

    private void setPainted() {
        isPainted = true;
    }
    private int robotCount;
    private LinkedList<Node> connections;
    private boolean isPainted;
    private boolean isActive;
}