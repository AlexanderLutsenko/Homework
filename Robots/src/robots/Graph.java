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

        nodes[0].consider(this);

        if (paintedRobots != 1 && unpaintedRobots != 1) {
            return true;
        } else {
            return false;
        }
    }

    public Node[] getNodes(){
        return nodes;
    }
    
    void paintedInc() {
        paintedRobots++;
    }

    void unpaintedInc() {
        unpaintedRobots++;
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

    public void consider(Graph graph) {
        isCounted = true;

        if (robotCount > 1) {
            robotCount = 0;
        }

        if (robotCount > 0) {
            if (isPainted) {
                graph.paintedInc();
            } else {
                graph.unpaintedInc();
            }
        }

        for (Node node : connections) {
            if (!node.isCounted) {
                node.consider(graph);
            }
        }
    }

    public boolean isPainted(){
        return isPainted;
    }
    
    public int getRobotCount(){
        return robotCount;
    }
    
    public boolean isConnectedWith(Node node){
        return connections.contains(node);
    }
    
    private void setPainted() {
        isPainted = true;
    }
    
    private int robotCount;
    private LinkedList<Node> connections;
    private boolean isPainted;
    private boolean isActive;
    private boolean isCounted;
}