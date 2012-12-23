package robots;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.LinkedList;
import java.util.Scanner;
import java.util.StringTokenizer;

public class Robots {

    public static void main(String[] args) {
        readData("input.txt");

        Graph graph = createGraph();
        graph.draw();

        if (graph.isSolvable()) {
            System.out.println("Можно уничтожить всех роботов");
        } else {
            System.out.println("Всех роботов уничтожить нельзя");
        }
    }

    public static void readData(String filename) {
        File input = new File(filename);
        try {
            Scanner scan = new Scanner(input);
            String str = scan.nextLine();
            StringTokenizer tokenizer = new StringTokenizer(str);
            nodeCount = tokenizer.countTokens();
            robots = new int[nodeCount];
            adjMatrix = new boolean[nodeCount][nodeCount];
            for (int j = 0; j < nodeCount; j++) {
                robots[j] = Integer.parseInt(tokenizer.nextToken());
            }
            str = scan.nextLine();
            for (int j = 0; j < nodeCount; j++) {
                str = scan.nextLine();
                tokenizer = new StringTokenizer(str);
                for (int i = 0; i < nodeCount; i++) {
                    adjMatrix[i][j] = convertToBoolean(Integer.parseInt(tokenizer.nextToken()));
                }
            }
            scan.close();
        } catch (FileNotFoundException e) {
            System.out.println("Файл не найден " + e);
            System.exit(0);
        } catch (Exception e) {
            System.out.println("Неверный формат ввода " + e);
            System.exit(0);
        }
    }

    public static Graph createGraph() {
        return new Graph(nodeCount, adjMatrix, robots);
    }

    private static boolean convertToBoolean(int number) {
        return number != 0;
    }
    private static int[] robots;
    private static boolean[][] adjMatrix;
    private static int nodeCount;
}
