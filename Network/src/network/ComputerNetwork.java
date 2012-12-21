package network;

import java.util.*;

public class ComputerNetwork {

    public ComputerNetwork(int compcount, boolean[][] adjmatrix, String[] os, Map securityRanks) {
        this.isDead = false;
        this.compcount = compcount;
        this.securityRanks = securityRanks;
        infectedComps = new LinkedList<>();
        computers = new Computer[compcount];

        for (int i = 0; i < compcount; i++) {
            computers[i] = new Computer();
            computers[i].id = i;
        }
        for (int i = 0; i < compcount; i++) {
            computers[i].init(os[i], adjmatrix, i);
        }
        System.out.println("Компьютерная сеть запущена");
    }

    public void infectComp(int infComp) { 
        computers[infComp].isInfected = true;        
        System.out.println("Компьютер " + infComp + " подхватил вирус из интернета!");
    }

    public String nextStep() {
        step++;

        infectedComps.removeAll(infectedComps);
        for (int j = 0; j < compcount; j++) {
            if (computers[j].isInfected) {
                infectedComps.add(computers[j]);
            }
        }

        if (!isDead && infectedComps.size() < compcount) {
            stepResult = "Шаг " + step + ":" + '\n';

            for (Computer comp : infectedComps) {
                comp.attack();
            }
        } else {
            stepResult = "Все компьютеры заражены!";
            isDead = true;
        }
        return stepResult;
    }

    public Boolean isDead() {
        return isDead;
    }

    public void setSecurityRanks(Map<String, Float> securityRanks) {
        this.securityRanks = securityRanks;
    }

    private class Computer {

        void init(String os, boolean[][] matrix, int id) {
            this.os = os;
            this.id = id;
            connections = new LinkedList<>();
            for (int j = 0; j < compcount; j++) {
                if (matrix[id][j] == true) {
                    connections.add(computers[j]);
                }
            }
        }

        private void attack() {
            if (isInfected) {
                for (Computer comp : connections) {
                    if (!comp.isInfected && comp.tryToInfect()) {
                        stepResult += ("    Компьютер " + this.id + " заразил компьютер " + comp.id + "(" + comp.os + ")" + '\n');
                    }
                }
            }
        }

        private boolean tryToInfect() {
            if (Math.random() < securityRanks.get(os)) {
                isInfected = true;
            }
            return isInfected;
        }
        private int id;
        private LinkedList<Computer> connections;
        private String os;
        private boolean isInfected;
    }
    private int compcount;
    private Map<String, Float> securityRanks;
    private Computer[] computers;
    private LinkedList<Computer> infectedComps;
    private int step;
    private String stepResult;
    private boolean isDead;
}
