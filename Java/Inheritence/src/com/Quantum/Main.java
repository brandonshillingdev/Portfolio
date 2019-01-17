package com.Quantum;

import java.awt.*;
import java.util.List;
import java.util.Random;

public class Main {

    public static void main(String[] args) {
        Outlander outlander = new Outlander(36);
        outlander.stear(45);
        outlander.accelerate(30);
        outlander.accelerate(20);
        outlander.accelerate(-42);
    }

}
